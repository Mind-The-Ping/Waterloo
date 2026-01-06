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

    public async Task<Result<int>> GetUserJourneyCountAsync(Guid userId)
    {
        try
        {
            var count = (int)await _journeyCollection
            .CountDocumentsAsync(j =>
                j.UserId == userId &&
                j.DeletedAt == null);

            return Result.Success(count);
        }
        catch (Exception ex)
        {
            var message = $"Could get the count of journeys for user {userId}.";

            _logger.LogError(ex, message);
            return Result.Failure<int>(message);
        }
    }

    public async Task<IEnumerable<AffectedUserDto>> GetUserIdsForAffectedJourneysAsync(
        Guid line,
        TimeOnly queryTime,
        DayOfWeek queryDay,
        IEnumerable<Disruption> disruptions)
    {

        var unmasked = ApplyDisruptionMasking(line, disruptions);
        var results = new List<AffectedUser>();

        foreach (var disruption in unmasked)
        {
            var queryStations = _routeRepository
              .GetStationsBetween(line, disruption.StartStationId, disruption.EndStationId)
              .Select(s => s.Id)
              .ToList();

            var masterRoute = _routeRepository
               .GetRoute(line, disruption.StartStationId, disruption.EndStationId)
               .Select(s => s.Id)
               .ToList();

            var journeys = await _journeyCollection
               .Find(x =>
                   x.LineId == line &&
                   queryTime >= x.StartTime &&
                   queryTime <= x.EndTime &&
                   x.DaysToCheck.Contains(queryDay) &&
                   disruption.Serverity >= x.Serverity
                   && x.DeletedAt == null)
               .ToListAsync();

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
                   disruption.Id,
                   _stationRepository.GetStationById(journey.StationIds.First())!,
                   _stationRepository.GetStationById(journey.StationIds.Last())!,
                   overlapStations,
                   journey.EndTime
                 )
                {
                    Severity = disruption.Serverity,
                    DisruptionSpanLength = queryStations.Count,
                    TotalJourneyStations = journey.StationIds.Count
                });
            }
        }

        return SelectBestPerJourney(results).Select(x => 
        new AffectedUserDto(
            x.Id,
            x.UserId,
            x.DisruptionId,
            x.StartStation,
            x.EndStation,
            x.AffectedStations,
            x.EndTime));
    }

    private IEnumerable<AffectedUser> SelectBestPerJourney(IEnumerable<AffectedUser> all)
    {
        return all
            .GroupBy(x => x.Id)
            .SelectMany(g => SelectBestForSingleJourney(g));
    }

    private IEnumerable<AffectedUser> SelectBestForSingleJourney(IEnumerable<AffectedUser> disruptionsForJourney)
    {
        var list = disruptionsForJourney.ToList();

        var segmentGroups = list
           .GroupBy(d => string.Join(",", d.AffectedStations.Select(s => s.Id)))
           .ToList();

        if(segmentGroups.Count == 1)
        {
            var group = segmentGroups.First();
            var full = group.Where(d => d.OverlapCount == d.TotalJourneyStations).ToList();

            if (full.Count > 0) {
                return PickBest(full);
            }

            return PickBest(group);
        }

        var selected = segmentGroups
           .SelectMany(g => PickBest(g))
           .ToList();

        return ApplyPerSegmentStationMasking(selected);
    }

    private IEnumerable<AffectedUser> PickBest(IEnumerable<AffectedUser> group) 
    {
        return group
            .GroupBy(x => x.Severity)
            .OrderByDescending(g => g.Key)
            .First()
            .OrderByDescending(x => x.OverlapCount)
            .ThenByDescending(x => x.DisruptionSpanLength)
            .Take(1);
    }

    private IEnumerable<Disruption> ApplyDisruptionMasking(
     Guid lineId,
     IEnumerable<Disruption> disruptions)
    {
        var list = disruptions.ToList();
        var toRemove = new HashSet<Disruption>();

        var allStationIds = new HashSet<Guid>();

        foreach (var d in list)
        {
            var seg = _routeRepository
                .GetStationsBetween(lineId, d.StartStationId, d.EndStationId)
                .Select(s => s.Id);

            foreach (var id in seg)
                allStationIds.Add(id);
        }

        var first = list.First();
        var referenceRoute = _routeRepository
            .GetRoute(lineId, first.StartStationId, first.EndStationId)
            .Select(s => s.Id)
            .ToList();

        var fullRoute = allStationIds
            .OrderBy(id => referenceRoute.IndexOf(id))
            .ToList();

        foreach (var a in list)
        {
            var aRange = _routeRepository
                .GetStationsBetween(lineId, a.StartStationId, a.EndStationId)
                .Select(s => s.Id)
                .ToList();

            int aDir = GetDirection(fullRoute, aRange);

            foreach (var b in list)
            {
                if (a == b) {
                    continue;
                }

                if (b.Serverity <= a.Serverity) {
                    continue;
                }

                var bRange = _routeRepository
                    .GetStationsBetween(lineId, b.StartStationId, b.EndStationId)
                    .Select(s => s.Id)
                    .ToList();

                int bDir = GetDirection(fullRoute, bRange);

                if (aDir == 0 || bDir == 0 || aDir != bDir) {
                    continue;
                }

                if (aRange.All(st => bRange.Contains(st))) {
                    toRemove.Add(a);
                }
            }
        }

        return list.Where(d => !toRemove.Contains(d));
    }

  

    private IEnumerable<AffectedUser> ApplyPerSegmentStationMasking(List<AffectedUser> selected)
    {
        var ordered = selected.OrderByDescending(s => s.Severity).ToList();

        var seenStations = new HashSet<Guid>();
        var result = new List<AffectedUser>();

        foreach (var disruption in ordered)
        {
            var maskedStations = disruption.AffectedStations
            .Where(st => !seenStations.Contains(st.Id))
            .ToList();

            foreach (var st in maskedStations) {
                seenStations.Add(st.Id);
            }

            result.Add(disruption with
            {
                AffectedStations = maskedStations
            });
        }

        return result.OrderByDescending(x => x.Severity);
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

        if (increasing) {
            return +1;
        }

        if (decreasing) {
            return -1;
        } 

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
