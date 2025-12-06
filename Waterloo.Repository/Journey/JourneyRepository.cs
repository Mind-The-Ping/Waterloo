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
