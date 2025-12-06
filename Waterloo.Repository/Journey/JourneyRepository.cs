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
        var stationRanges = new Dictionary<Guid, List<Model.Station>>();
        foreach (var disruption in disruptions)
        {
            var stations = _routeRepository.GetStationsBetween(
                disruption.Line.Id,
                disruption.StartStation.Id,
                disruption.EndStation.Id);

            stationRanges[disruption.Id] = [.. stations];
        }

        var seen = new HashSet<string>();
        var overlaps = new List<List<Model.Station>>();

        foreach (var a in disruptions)
        {
            var aStations = stationRanges[a.Id];

            foreach (var b in disruptions)
            {
                if (a.Id == b.Id) {
                    continue;
                }

                var bStations = stationRanges[b.Id];
                var overlap = GetOrderedOverlap(aStations, bStations);

                if (overlap.Count == 0) {
                    continue;
                }

                bool sameDirection =
                    aStations.IndexOf(overlap.First()) < aStations.IndexOf(overlap.Last()) &&
                    bStations.IndexOf(overlap.First()) < bStations.IndexOf(overlap.Last());

                if(!sameDirection) {
                    continue;
                }

                var key = string.Join(",", overlap.Select(s => s.Id));

                if (seen.Add(key)) {
                    overlaps.Add(overlap);
                }
            }
        }

        if(overlaps.Count == 0) {
            return disruptions;
        }

        var finalDisruptions = new List<Disruption>();
        foreach (var overlap in overlaps)
        {
            var first = overlap.First();
            var last = overlap.Last();

            var candidates = disruptions
            .Where(d =>
            {
                var range = stationRanges[d.Id];
                var startIndex = range.FindIndex(s => s.Id == first.Id);
                var endIndex = range.FindIndex(s => s.Id == last.Id);

                return startIndex != -1 && endIndex != -1 && startIndex <= endIndex;
            })
            .ToList();

            var worst = candidates
            .OrderByDescending(d => d.Severity)
            .First();

            var id = Guid.NewGuid();
            var newDisruption = new Disruption(
               id,
               worst.Line,
               first,
               last,
               worst.Description,
               worst.Severity,
               GuidHelper.GuidFromString($"{id}-{worst.Severity}"),
               worst.DescriptionId,
               worst.LastUpdatedUtc
           );

            finalDisruptions.Add(newDisruption);
        }

        var cleaned = new List<Disruption>();
        foreach (var original in disruptions)
        {
            var range = stationRanges[original.Id];
            var overlapIds = overlaps
               .SelectMany(o => o.Select(s => s.Id))
               .ToHashSet();

            var remaining = range
               .Where(s => !overlapIds.Contains(s.Id))
               .ToList();

            if (remaining.Count == 0) {
                continue;
            }

            var segments = new List<List<Model.Station>>();
            var current = new List<Model.Station>();

            foreach (var station in remaining)
            {
                if (current.Count == 0) {
                    current.Add(station);
                }
                else
                {
                    var prevIndex = range.FindIndex(s => s.Id == current.Last().Id);
                    var thisIndex = range.FindIndex(s => s.Id == station.Id);

                    if (thisIndex == prevIndex + 1) {
                        current.Add(station);
                    }
                    else {
                        segments.Add(current);
                        current = [station];
                    }
                }
            }

            if (current.Count > 0) {
                segments.Add(current);
            }

            foreach (var segment in segments)
            {
                var id = Guid.NewGuid();

                cleaned.Add(new Disruption(
                    id,
                    original.Line,
                    segment.First(),
                    segment.Last(),
                    original.Description,
                    original.Severity,
                    GuidHelper.GuidFromString($"{id}-{original.Severity}"),
                    original.DescriptionId,
                    original.LastUpdatedUtc
                ));
            }
        }

        return finalDisruptions.Concat(cleaned);
    }

    private static List<Model.Station> GetOrderedOverlap(List<Model.Station> a, List<Model.Station> b)
    {
        List<Model.Station> result = [];

        for (int i = 0; i < a.Count; i++)
        {
            int bi = b.FindIndex(s => s.Id == a[i].Id);
            if (bi == -1)
                continue;

            List<Model.Station> temp = [];
            int ia = i;
            int ib = bi;

            while (ia < a.Count && ib < b.Count && a[ia].Id == b[ib].Id)
            {
                temp.Add(a[ia]);
                ia++;
                ib++;
            }

            if (temp.Count == 0) {
                continue;
            }

            if (temp.Count > result.Count) {
                result = temp;
            }
        }

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
}
