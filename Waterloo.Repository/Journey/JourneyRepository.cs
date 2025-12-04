using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Waterloo.Model;
using Waterloo.Options;
using Waterloo.Repository.Line;
using Waterloo.Repository.Route;
using Waterloo.Repository.Station;

namespace Waterloo.Repository.Journey;

public class JourneyRepository(
    IOptions<DatabaseOptions> databaseOptions,
    IMongoDatabase mongoDatabase,
    LineRepository lineRepository,
    RouteRepository routeRepository,
    StationRepository stationRepository,
    ILogger<JourneyRepository> logger) : IJourneyRepository
{
    private readonly LineRepository _lineRepository = lineRepository;
    private readonly RouteRepository _routeRepository = routeRepository;
    private readonly StationRepository _stationRepository = stationRepository;
    private readonly ILogger<JourneyRepository> _logger = logger;
    private readonly IMongoCollection<Model.Journey> _journeyCollection =
        mongoDatabase.GetCollection<Model.Journey>(databaseOptions.Value.Collection);

    private readonly static TimeZoneInfo _londonTimeZone =  
        TimeZoneInfo.FindSystemTimeZoneById("Europe/London");

    public async Task<Result> AddJourneyAsync(
        Guid userId, 
        Guid lineId, 
        IEnumerable<Guid> stationIds,
        TimeOnly startTime,
        TimeOnly endTime, 
        IEnumerable<DayOfWeek> daysToCheck, 
        Serverity serverity)
    {
        var journey = new Model.Journey
        {
            UserId = userId,
            LineId = lineId,
            StationIds = [.. stationIds],
            StartTime = ConvertToUtc(startTime),
            EndTime = ConvertToUtc(endTime),
            DaysToCheck = [.. daysToCheck],
            Serverity = serverity,
            CreatedAt = DateTime.UtcNow,
        };

        try {
            await _journeyCollection.InsertOneAsync(journey);
        }
        catch(Exception ex)
        {
            var message = $"Could not save journey for user: {userId}.";

            _logger.LogError(ex, message);
            return Result.Failure(message);
        }

        return Result.Success();
    }

    public async Task<IEnumerable<JourneyReturn>> GetJourneysByUserIdAsync(Guid userId)
    {
        var journeys = await _journeyCollection
        .Find(j => j.UserId == userId && 
              j.DeletedAt == null)
        .ToListAsync();

        var results = new List<JourneyReturn>();

        foreach (var journey in journeys)
        {
            var utcStartDateTime = journey.CreatedAt.Date.Add(journey.StartTime.ToTimeSpan());
            var utcEndDateTime = journey.CreatedAt.Date.Add(journey.EndTime.ToTimeSpan());

            var localStart = TimeZoneInfo.ConvertTimeFromUtc(utcStartDateTime, _londonTimeZone);
            var localEnd = TimeZoneInfo.ConvertTimeFromUtc(utcEndDateTime, _londonTimeZone);

            results.Add(new JourneyReturn(
               journey.Id,
               _lineRepository.GetLineById(journey.LineId)!,
               _stationRepository.GetStationById(journey.StationIds.First())!,
               _stationRepository.GetStationById(journey.StationIds.Last())!,
               TimeOnly.FromDateTime(localStart),
               TimeOnly.FromDateTime(localEnd),
               journey.DaysToCheck,
               journey.Serverity));
        }

        return results;
    }

    public async Task<Result> RemoveJourneyAsync(Guid id)
    {
        try
        {
            var update = Builders<Model.Journey>.Update
            .Set(x => x.DeletedAt, DateTime.UtcNow);

            var updatedUser = await _journeyCollection.FindOneAndUpdateAsync(
             x => x.Id == id,
             update,
             new FindOneAndUpdateOptions<Model.Journey>
             {
                 ReturnDocument = ReturnDocument.After
             });

            if (updatedUser == null)
            {
                _logger.LogInformation("Could not find {id} to delete.", id);
                return Result.Success();
            }

            return Result.Success();
        }
        catch(Exception ex)
        {
            var message = $"Could not delete journey {id}.";

            _logger.LogError(ex, message);
            return Result.Failure(message);
        }
    }

    public async Task<Result> RemoveJourneyByUserIdAsync(Guid userId, DateTime deletedAt)
    {
        try
        {
            var update = Builders<Model.Journey>.Update
            .Set(x => x.DeletedAt, deletedAt);

            var result = await _journeyCollection.UpdateManyAsync(
              j => j.UserId == userId,
              update
            );

            _logger.LogInformation("Marked {count} journeys as deleted for user {userId}.",
             result.ModifiedCount, userId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            var message = $"Could not delete journeys for user {userId}.";

            _logger.LogError(ex, message);
            return Result.Failure(message);
        }
    }

    public async Task<IEnumerable<AffectedUser>> GetUserIdsForAffectedJourneysAsync(
    Guid line,
    Guid startStation,
    Guid endStation,
    Serverity serverity,
    TimeOnly queryTime,
    DayOfWeek queryDay)
    {
        var queryStations = _routeRepository
           .GetStationsBetween(line, startStation, endStation)
           .Select(s => s.Id)
           .ToList();

        var masterRoute = _routeRepository
           .GetRoute(line, startStation, endStation)
           .Select(s => s.Id)
           .ToList();

        var journeys = await _journeyCollection
            .Find(x =>
                x.LineId == line &&
                queryTime >= x.StartTime &&
                queryTime <= x.EndTime &&
                x.DaysToCheck.Contains(queryDay) &&
                serverity >= x.Serverity
                && x.DeletedAt == null)
            .ToListAsync();

        var results = new List<AffectedUser>(journeys.Count);

        foreach (var journey in journeys)
        {
            if (!journey.StationIds.Any(queryStations.Contains)) {
                continue;
            }

            int jDir = GetDirection(masterRoute, [.. journey.StationIds]);
            int segDir = GetDirection(masterRoute, queryStations);

            if (jDir == 0 || jDir != segDir) {
                continue;
            }

            var overlapStations = journey.StationIds
               .Where(queryStations.Contains)
               .OrderBy(id => masterRoute.IndexOf(id))
               .Select(id => _stationRepository.GetStationById(id)!)
               .ToList();

            results.Add(new AffectedUser(
                journey.Id,
                journey.UserId,
                _stationRepository.GetStationById(journey.StationIds.First())!,
                _stationRepository.GetStationById(journey.StationIds.Last())!,
                overlapStations,
                journey.EndTime
            ));
        }

        return results
            .GroupBy(x => x.Id)
            .Select(g => g.First());
    }

    public async Task<IEnumerable<Disruption>> SegmentDisruptionsAsync(IEnumerable<Disruption> disruptions)
    {
        var disruptionList = disruptions?.ToList() ?? new List<Disruption>();
        if (disruptionList.Count == 0)
            return disruptionList;

        var lineId = disruptionList.First().Line.Id;

        if (!_routeRepository.Lines.TryGetValue(lineId, out var lineData) || lineData == null)
            return disruptionList;

        var first = disruptionList.First();

        var masterRouteStations = _routeRepository
            .GetRoute(lineId, first.StartStation.Id, first.EndStation.Id)
            .Select(s => s.Id)
            .ToList();

        if (masterRouteStations.Count == 0)
            return disruptionList;

        var forward = new List<Disruption>();
        var reverse = new List<Disruption>();
        var passthrough = new List<Disruption>();

        foreach (var d in disruptionList)
        {
            int si = masterRouteStations.IndexOf(d.StartStation.Id);
            int ei = masterRouteStations.IndexOf(d.EndStation.Id);

            if (si == -1 || ei == -1)
            {
                passthrough.Add(d);
                continue;
            }

            if (si <= ei) {
                forward.Add(d);
            }
            else
            {
                reverse.Add(d);
            }
        }

        var result = new List<Disruption>();
        if (forward.Count > 0)
        {
            result.AddRange(SegmentCore(forward, masterRouteStations));
        }

        if (reverse.Count > 0)
        {
            var mappedToForward = reverse
                .Select(d => new Disruption(
                    id: d.Id,
                    line: d.Line,
                    startStation: d.EndStation,    // SWAP
                    endStation: d.StartStation,    // SWAP
                    description: d.Description,
                    severity: d.Severity,
                    severityId: d.SeverityId,
                    descriptionId: d.DescriptionId,
                    lastUpdatedUtc: d.LastUpdatedUtc
                ))
                .ToList();

            var segmentedForward = SegmentCore(mappedToForward, masterRouteStations);

            foreach (var seg in segmentedForward)
            {
                var flipped = new Disruption(
                    id: Guid.NewGuid(),
                    line: seg.Line,
                    startStation: seg.EndStation,
                    endStation: seg.StartStation,
                    description: seg.Description,
                    severity: seg.Severity,
                    severityId: seg.SeverityId,
                    descriptionId: seg.DescriptionId,
                    lastUpdatedUtc: seg.LastUpdatedUtc
                );

                result.Add(flipped);
            }
        }

        result.AddRange(passthrough);

        result = [.. result
            .OrderBy(d =>
            {
                var ix = masterRouteStations.IndexOf(d.StartStation.Id);
                return ix == -1 ? int.MaxValue : ix;
            })];

        return result;
    }



    private static int GetDirection(List<Guid> master, List<Guid> partial)
    {
        var indexes = partial.Select(station => master.IndexOf(station)).ToList();

        if (indexes.Any(i => i == -1)) {
            return 0;
        }

        bool increasing = true;
        bool decreasing = true;

        for (int i = 0; i < indexes.Count - 1; i++)
        {
            if (indexes[i] >= indexes[i + 1]) {
                increasing = false;
            }

            if (indexes[i] <= indexes[i + 1]) {
                decreasing = false;
            }
        }

        if (increasing) return +1;
        if (decreasing) return -1;

        return 0;
    }

   

    private static TimeOnly ConvertToUtc(TimeOnly timeOnly)
    {
        var londonDateTime = DateTime.SpecifyKind(
            DateTime.Today.Add(timeOnly.ToTimeSpan()),
            DateTimeKind.Unspecified
        );

        var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(londonDateTime, _londonTimeZone);

        return TimeOnly.FromDateTime(utcDateTime);
    }

    private IEnumerable<Disruption> SegmentCore(
    List<Disruption> disruptions,
    List<Guid> masterRouteStations)
    {
        var result = new List<Disruption>();
        var intervals = new List<Interval>();

        foreach (var d in disruptions)
        {
            int startIndex = masterRouteStations.IndexOf(d.StartStation.Id);
            int endIndex = masterRouteStations.IndexOf(d.EndStation.Id);

            if (startIndex == -1 || endIndex == -1)
            {
                result.Add(d);
                continue;
            }

            if (startIndex > endIndex) {
                (startIndex, endIndex) = (endIndex, startIndex);
            }

            intervals.Add(new Interval(startIndex, endIndex, d));
        }

        if (intervals.Count == 0)
            return result;

        int minIndex = intervals.Min(i => i.Start);
        int maxIndex = intervals.Max(i => i.End);

        var winning = new Interval?[masterRouteStations.Count];

        for (int idx = minIndex; idx <= maxIndex; idx++)
        {
            var covering = intervals
                .Where(i => i.Start <= idx && i.End >= idx)
                .ToList();

            if (covering.Count == 0)
                continue;

            var chosen = covering
                .OrderByDescending(i => (int)i.Source.Severity)
                .ThenBy(i => i.End - i.Start)
                .First();

            winning[idx] = chosen;
        }

        Interval? current = null;
        int currentStart = -1;

        for (int idx = minIndex; idx <= maxIndex; idx++)
        {
            var chosen = winning[idx];

            if (chosen is null)
            {
                if (current != null)
                {
                    AddSegment(current, currentStart, idx - 1, masterRouteStations, result);
                    current = null;
                }

                continue;
            }

            if (!ReferenceEquals(current, chosen))
            {
                if (current != null) {
                    AddSegment(current, currentStart, idx - 1, masterRouteStations, result);
                }

                current = chosen;
                currentStart = idx;
            }
        }

        if (current != null) {
            AddSegment(current, currentStart, maxIndex, masterRouteStations, result);
        }

        return result;
    }

    private sealed class Interval
    {
        public int Start { get; }
        public int End { get; }
        public Disruption Source { get; }

        public Interval(int start, int end, Disruption source)
        {
            Start = start;
            End = end;
            Source = source;
        }
    }

    private void AddSegment(
        Interval interval,
        int fromIndex,
        int toIndex,
        List<Guid> routeStations,
        List<Disruption> result)
    {
        var startStationId = routeStations[fromIndex];
        var endStationId = routeStations[toIndex];

        var startStation = _stationRepository.GetStationById(startStationId)
            ?? throw new Exception($"Station not found: {startStationId}");
        var endStation = _stationRepository.GetStationById(endStationId)
            ?? throw new Exception($"Station not found: {endStationId}");

        var s = interval.Source;

        var id = Guid.NewGuid();
        var segment = new Disruption(
            id: id,
            line: s.Line,
            startStation: startStation,
            endStation: endStation,
            description: s.Description,
            severity: s.Severity,
            severityId: GuidHelper.GuidFromString($"{id}-{s.Severity}"),
            descriptionId: s.DescriptionId,
            lastUpdatedUtc: s.LastUpdatedUtc
        );

        result.Add(segment);
    }
}
