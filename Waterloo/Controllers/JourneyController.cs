using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Waterloo.Dtos;
using Waterloo.Journey;
using Waterloo.Repository.Line;
using Waterloo.Repository.Route;

namespace Waterloo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class JourneyController(LineRepository lineRepository,
                               RouteRepository routeRepository,
                               IJourneyRepository journeyRepository) : ControllerBase
{
    private readonly LineRepository _lineRepository = lineRepository;
    private readonly RouteRepository _routeRepository = routeRepository;
    private readonly IJourneyRepository _journeyRepository = journeyRepository;

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create(JourneyDto journeyDto)
    {
        var subValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(subValue, out var userId)) {
            return BadRequest("Sorry, you need to login to access this endpoint");
        }

        var line = _lineRepository.GetLineById(journeyDto.LineId);
        
        if (line == null) {
            return BadRequest($"Sorry your line Id is Invalid {journeyDto.LineId}");
        }

        var stations = _routeRepository.GetStationsBetween(
            line.Id, 
            journeyDto.StartStationId, 
            journeyDto.EndStationId);

        if (stations == null || !stations.Any()) {
            return BadRequest($"Sorry either your start station id is invalid {journeyDto.StartStationId} " +
                              $"or end station id is {journeyDto.EndStationId}");
        }

        var result = await _journeyRepository.AddJourneyAsync(
            userId, 
            line.Id, 
            stations.Select(x => x.Id), 
            journeyDto.StartTime, 
            journeyDto.EndTime,
            journeyDto.DaysToCheck,
            journeyDto.Serverity);


        return result ? Ok() : 
                        Problem(detail: "Database save failed due to unexpected error", 
                                statusCode: StatusCodes.Status500InternalServerError);
    }

    [Authorize]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _journeyRepository.RemoveJourneyAsync(id);

        if (!result) {
            return BadRequest($"Sorry couldn't delete {id}");
        }

        return Ok(id);
    }

    [Authorize]
    [HttpGet("affectedJourneys")]
    public async Task<IActionResult> AffectedJourneys([FromQuery] AffectedJourneysDto affectedJourneysDto)
    {
        var result = await _journeyRepository.GetUserIdsForAffectedJourneysAsync(
            affectedJourneysDto.LineId,
            affectedJourneysDto.StartStationId,
            affectedJourneysDto.EndStationId,
            affectedJourneysDto.Serverity,
            affectedJourneysDto.QueryTime,
            affectedJourneysDto.QueryDay);

        if(result == null || !result.Any()) {
            return NotFound();
        }

        return Ok(result);
    }
}
