using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Waterloo.Dtos;
using Waterloo.Model;

namespace Waterloo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JourneyController : ControllerBase
{
    private readonly ILogger<JourneyController> _logger;
    private readonly JourneyOrchestrator _orchestrator;

    public JourneyController(
        JourneyOrchestrator orchestrator, 
        ILogger<JourneyController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orchestrator = orchestrator ?? throw new ArgumentNullException(nameof(orchestrator));
    }


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

       
        var result = await _orchestrator.CreateJourneyAsync(userId, journeyDto);

        if(result.IsFailure) {
            return BadRequest(result.Error);
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

        var result = await _orchestrator.GetJourneysByUserIdAsync(userId);

        _logger.LogInformation("Successfully got journeys for user: {userId}", userId);
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Begin deleting journey {Id}.", id);

        var result = await _orchestrator.RemoveJourneyAsync(id);

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

        var result = await _orchestrator.GetUserIdsForAffectedJourneysAsync(
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
