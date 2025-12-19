using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Waterloo.Dtos;
using Waterloo.Model;
using Waterloo.Repository.Journey;
using Waterloo.Repository.Line;
using Waterloo.Repository.Route;

namespace Waterloo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JourneyController(LineRepository lineRepository,
                               RouteRepository routeRepository,
                               ILogger<JourneyController> logger,
                               IJourneyRepository journeyRepository) : ControllerBase
{
    private readonly LineRepository _lineRepository = lineRepository;
    private readonly RouteRepository _routeRepository = routeRepository;
    private readonly IJourneyRepository _journeyRepository = journeyRepository;
    private readonly ILogger<JourneyController> _logger = logger;

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create(JourneyDto journeyDto)
    {
        var subValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(subValue, out var userId)) 
        {
            _logger.LogError("User could not log in with {userId}", subValue);
            return BadRequest("Can not access endpoint without logging in.");
        }

        _logger.LogInformation("Begin create journey for {userId}", userId);

        var line = _lineRepository.GetLineById(journeyDto.LineId);
        
        if (line == null) 
        {
            var message = $"Line Id is Invalid {journeyDto.LineId}.";

            _logger.LogError(message);
            return BadRequest(message);
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
            return BadRequest(message);
        }

        var result = await _journeyRepository.AddJourneyAsync(
            userId, 
            line.Id, 
            stations.Select(x => x.Id), 
            journeyDto.StartTime, 
            journeyDto.EndTime,
            journeyDto.DaysToCheck,
            journeyDto.Serverity);

        if(result.IsFailure) {
            return Problem(result.Error);
        }

        _logger.LogInformation("Successfully created journey for {userId}", userId);

        return Ok();
    }

    [Authorize]
    [HttpGet("getByUserId")]
    public async Task<IActionResult> GetJourneysByUserId()
    {
        var subValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(subValue, out var userId))
        {
            _logger.LogError("User could not log in with {userId}", subValue);
            return BadRequest("Can not access endpoint without logging in.");
        }

        _logger.LogInformation("Begin getting journeys for user: {userId}.", userId);

        var result = await _journeyRepository.GetJourneysByUserIdAsync(userId);

        _logger.LogInformation("Successfully got journeys for user: {userId}", userId);
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Begin deleting journey {Id}.", id);

        var result = await _journeyRepository.RemoveJourneyAsync(id);

        if (result.IsFailure) {
            return BadRequest(result.Error);
        }

        _logger.LogInformation("Successfully deleted journey {Id}", id);
        return Ok(id);
    }

    [Authorize]
    [HttpPost("affectedJourneys")]
    public async Task<IActionResult> AffectedJourneys(AffectedJourneysDto affectedJourneysDto)
    {
        _logger.LogInformation(
            "Begin getting affected journeys for line {lineId} on {day} at time {windowTime}",
            affectedJourneysDto.LineId,
            affectedJourneysDto.QueryDay,
            affectedJourneysDto.QueryTime);

        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            affectedJourneysDto.LineId,
            affectedJourneysDto.QueryTime,
            affectedJourneysDto.QueryDay,
            affectedJourneysDto.Disruptions
            .Select(x => new Disruption(
                x.Id,
                x.StartStationId,
                x.EndStationId,
                x.Serverity)));


        if (result.Any())
        {
            _logger.LogInformation(
              "Successfully got affected journeys for line {lineId} on {day} at time {windowTime}",
              affectedJourneysDto.LineId,
              affectedJourneysDto.QueryDay,
              affectedJourneysDto.QueryTime);
        }

        return Ok(result);
    }
}
