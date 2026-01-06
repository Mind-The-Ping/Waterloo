using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Waterloo.Clients.StanmoreClient;
using Waterloo.Dtos;
using Waterloo.Model;
using Waterloo.Model.Options;
using Waterloo.Repository.Journey;
using Waterloo.Repository.Line;
using Waterloo.Repository.Route;

namespace Waterloo;

public class JourneyOrchestrator
{
    private readonly LineRepository _lineRepository;
    private readonly RouteRepository _routeRepository;
    private readonly IJourneyRepository _journeyRepository;
    private readonly IStanmoreClient _stanmoreClient;
    private readonly JourneyOptions _journeyOptions;
    private readonly ILogger<JourneyOrchestrator> _logger;

    public JourneyOrchestrator(
        LineRepository lineRepository,
        RouteRepository routeRepository,
        IJourneyRepository journeyRepository,
        IStanmoreClient stanmoreClient,
        IOptions<JourneyOptions> journeyOptions,  
        ILogger<JourneyOrchestrator> logger)
    {
        _lineRepository = lineRepository ?? 
            throw new ArgumentNullException(nameof(lineRepository));

        _routeRepository = routeRepository ?? 
            throw new ArgumentNullException(nameof(routeRepository));

        _journeyRepository = journeyRepository ?? 
            throw new ArgumentNullException(nameof(journeyRepository));

        _stanmoreClient = stanmoreClient ?? 
            throw new ArgumentNullException(nameof(stanmoreClient));

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _journeyOptions = journeyOptions.Value ?? 
            throw new ArgumentNullException(nameof(journeyOptions));
    }

    public async Task<Result> CreateJourneyAsync(Guid userId, JourneyDto journeyDto)
    {
        var line = _lineRepository.GetLineById(journeyDto.LineId);

        if (line == null)
        {
            var message = $"Line Id is Invalid {journeyDto.LineId}.";

            _logger.LogError(message);
            return Result.Failure(message);
        }

        var isPremiumUser = await _stanmoreClient.IsUserPremiumAsync(userId);
        if (isPremiumUser.IsFailure) {
            return Result.Failure(isPremiumUser.Error);
        }

        var allowedLimit = isPremiumUser.Value
        ? _journeyOptions.PremiumSaveLimit
        : _journeyOptions.FreeSaveLimit;

        var userJourneyCount = await _journeyRepository.GetUserJourneyCountAsync(userId);
        if (userJourneyCount.IsFailure) {
            return Result.Failure(userJourneyCount.Error);
        }

        if (userJourneyCount.Value >= allowedLimit)
        {
            var message = isPremiumUser.Value
                ? "You have reached the maximum number of saved journeys for your subscription."
                : "Free users can only save one journey. Upgrade to Premium to save more.";

            _logger.LogInformation(
                "User {UserId} blocked from creating journey. Count={Count}, Limit={Limit}",
                userId,
                userJourneyCount.Value,
                allowedLimit);

            return Result.Failure(message);
        }

        var stations = _routeRepository.GetStationsBetween(
            line.Id,
            journeyDto.StartStationId,
            journeyDto.EndStationId);

        if (stations == null || !stations.Any())
        {
            var message = $"Either your start station id is invalid {journeyDto.StartStationId} " +
                          $"or end station id is {journeyDto.EndStationId}.";

            _logger.LogError(message);
            return Result.Failure(message);
        }

        var result = await _journeyRepository.AddJourneyAsync(
            userId,
            line.Id,
            stations.Select(x => x.Id),
            journeyDto.StartTime,
            journeyDto.EndTime,
            journeyDto.DaysToCheck,
            journeyDto.Serverity);

        if (result.IsFailure) {
            return result;
        }

        return Result.Success();
    }

    public async Task<IEnumerable<JourneyReturn>> GetJourneysByUserIdAsync(Guid userId) =>
        await _journeyRepository.GetJourneysByUserIdAsync(userId);

    public async Task<Result> RemoveJourneyAsync(Guid id) =>
        await _journeyRepository.RemoveJourneyAsync(id);

    public async Task<IEnumerable<AffectedUserDto>> GetUserIdsForAffectedJourneysAsync(
       Guid line,
       TimeOnly dateTime,
       DayOfWeek queryDay,
       IEnumerable<Disruption> disruptions) =>
        await _journeyRepository.GetUserIdsForAffectedJourneysAsync(line, dateTime, queryDay, disruptions);
}
