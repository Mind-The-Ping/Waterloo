using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Waterloo.Dtos;
using Waterloo.Model;
using Waterloo.Repository.Line;
using Waterloo.Repository.Station;

namespace Waterloo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MapController(
    LineRepository lineRepository, 
    StationRepository stationRepository,
    ILogger<MapController> logger) : ControllerBase
{
    private readonly LineRepository _lineRepository = lineRepository;
    private readonly StationRepository _stationRepository = stationRepository;
    private readonly ILogger<MapController> _logger = logger;

    [Authorize]
    [HttpGet("lines")]
    public IActionResult Lines() 
    {
        _logger.LogInformation("Begin to get all lines.");

        var result = _lineRepository.GetAll();

        _logger.LogInformation("Succesfully got all lines.");
        return Ok(result);
    }

    [Authorize]
    [HttpGet("lineByName")]
    public IActionResult Line(string name) 
    {
        _logger.LogInformation("Begin to get line by name {name}.", name);
        var result = _lineRepository.GetLineByName(name);

        if(result is null) 
        {
            var message = $"Could not get line {name}.";

            _logger.LogError(message);
            return BadRequest(message);
        }

        _logger.LogInformation("Successfully got line by name {line}.", result);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("lineById")]
    public IActionResult Line(Guid id)
    {
        _logger.LogInformation("Begin to get line by id {id}.", id);

        var line = _lineRepository.GetLineById(id);

        if (line is null)
        {
            var message = $"Could not get line {id}.";

            _logger.LogError(message);
            return BadRequest(message);
        }

        _logger.LogInformation("Successfully got line {line}.", line);

        return Ok(line);
    }

    [Authorize]
    [HttpGet("stations")]
    public IActionResult Stations(Guid id)
    {
        _logger.LogInformation("Begin to get stations by line {id}.", id);

        var stations = _stationRepository.GetByLine(id);

        if(!stations.Any()) 
        {
            var message = $"Sorry couldn't find stations for {id}.";
            
            _logger.LogError(message);
            return BadRequest(message);
        }

        return Ok(stations);
    }

    [Authorize]
    [HttpPost("toStations")]
    public IActionResult ToStations([FromBody] ToStationDto toStationDto)
    {
        _logger.LogInformation("Begin to get to stations by line {lineId} and from station {fromStationId}.",
            toStationDto.LineId, toStationDto.FromStationId);

        var stations = _stationRepository.GetByToStation(
            toStationDto.LineId, 
            toStationDto.FromStationId);

        if (!stations.Any())
        {
            var message = $"Sorry couldn't find stations for line {toStationDto.LineId} and from station {toStationDto.FromStationId}.";

            _logger.LogError(message);
            return BadRequest(message);
        }

        return Ok(stations);
    }

    [Authorize]
    [HttpGet("station")]
    public IActionResult Station(string name)
    {
        _logger.LogInformation("Begin to get station by name {id}.", name);

        var station = _stationRepository.GetStationByName(name);

        if(station is null) 
        {
            var message = $"Sorry couldn't find station {name}.";

            _logger.LogError(message);
            return BadRequest(message);
        }

        _logger.LogInformation("Successfully got station {station}.", station);

        return Ok(station);
    }

    [Authorize]
    [HttpGet("stationById")]
    public IActionResult Station(Guid id)
    {
        _logger.LogInformation("Begin to get station by id {id}.", id);

        var station = _stationRepository.GetStationById(id);

        if (station is null)
        {
            var message = $"Sorry couldn't find station {id}.";

            _logger.LogError(message);
            return BadRequest(message);
        }

        _logger.LogInformation("Successfully got station {station}.", station);

        return Ok(station);
    }
}
