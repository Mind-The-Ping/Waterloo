using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Waterloo.Database;
using Waterloo.Model;
using Waterloo.Repository.Route;
using Waterloo.Repository.Station;

namespace Waterloo.Journey;

public class JourneyRepository(
    JourneyDbContext journeyDbContext, 
    RouteRepository routeRepository,
    StationRepository stationRepository,
    ILogger<JourneyRepository> logger) : IJourneyRepository
{
    private readonly RouteRepository _routeRepository = routeRepository;
    private readonly JourneyDbContext _journeyDbContext = journeyDbContext;
    private readonly StationRepository _stationRepository = stationRepository;
    private readonly ILogger<JourneyRepository> _logger = logger;

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
            Serverity = serverity
        };

        try
        {
            await _journeyDbContext.Journeys.AddAsync(journey);
            await _journeyDbContext.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            var message = $"Could not save journey for user: {userId}.";

            _logger.LogError(ex, message);
            return Result.Failure(message);
        }

        return Result.Success();
    }

    public IEnumerable<JourneyReturn> GetJourneysByUserId(Guid userId) =>
     _journeyDbContext.Journeys.Where(x => x.UserId == userId)
        .AsEnumerable()
        .Select(x => new JourneyReturn(
            x.StationIds.First(), 
            x.StationIds.Last(),
            x.StartTime,
            x.EndTime,
            x.DaysToCheck,
            x.Serverity));

    public async Task<Result> RemoveJourneyAsync(Guid id)
    {
        var journey = _journeyDbContext.Journeys.SingleOrDefault(x => x.Id == id);

        if(journey == null)
        {
            _logger.LogInformation("Could not find {id} to delete.", id);
            return Result.Success();
        }

        try
        {
            _journeyDbContext.Remove(journey);
            await _journeyDbContext.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            var message = $"Could not delete journey {id}.";

            _logger.LogError(ex, message);
            return Result.Failure(message);
        }

        return Result.Success();
    }

    public async Task<IEnumerable<AffectedUser>> GetUserIdsForAffectedJourneysAsync(
        Guid line,
        Guid startStation,
        Guid endStation,
        Serverity serverity,
        TimeOnly queryTime,
        DayOfWeek queryDay)
    {
        var queryStations = _routeRepository.GetStationsBetween(line, startStation, endStation).Select(x => x.Id);

        var filteredJourneys = await _journeyDbContext.Journeys
             .Where(x => 
             x.LineId == line &&
             queryTime >= x.StartTime && 
             queryTime <= x.EndTime && 
             x.DaysToCheck.Contains(queryDay) &&
             serverity >= x.Serverity)
             .ToListAsync();

        var matchingUserIds = filteredJourneys
              .Where(j => DoesJourneyOverlapSegment(
                  line, 
                  startStation, 
                  endStation, 
                  [.. j.StationIds], 
                  [.. queryStations]))
              .Select(j => new AffectedUser(
                  j.UserId,
                      _stationRepository.GetStationById(startStation) ?? throw new InvalidOperationException($"Station {startStation} not found"),
    _stationRepository.GetStationById(endStation) ?? throw new InvalidOperationException($"Station {endStation} not found"),
                  j.EndTime))
              .Distinct();


        return matchingUserIds;
    }


    bool DoesJourneyOverlapSegment(
        Guid line,
        Guid startStation, 
        Guid endStation,
        List<Guid> journeyStations, 
        List<Guid> affectedSegment)
    {
        if (!journeyStations.Any(affectedSegment.Contains)) {
            return false;
        }

        var masterLine = _routeRepository.GetRoute(line, startStation, endStation).Select(x => x.Id).ToList();

        int dirA = GetDirection(masterLine, journeyStations);
        int dirB = GetDirection(masterLine, affectedSegment);

        return dirA != 0 && dirA == dirB;
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
