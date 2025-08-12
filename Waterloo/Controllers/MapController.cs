using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Waterloo.Repository.Line;
using Waterloo.Repository.Station;

namespace Waterloo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MapController(LineRepository lineRepository, StationRepository stationRepository) : ControllerBase
{
    private readonly LineRepository _lineRepository = lineRepository;
    private readonly StationRepository _stationRepository = stationRepository;

    [Authorize]
    [HttpGet("lines")]
    public IActionResult Lines() {
        return Ok(_lineRepository.GetAll());
    }

    [Authorize]
    [HttpGet("stations")]
    public IActionResult Stations(Guid id)
    {
        var stations = _stationRepository.GetByLine(id);

        if(!stations.Any()) {
            return BadRequest($"Sorry couldn't find stations for {id}");
        }

        return Ok(stations);
    }

    [Authorize]
    [HttpGet("station")]
    public IActionResult Station(string name)
    {
        var station = _stationRepository.GetStationByName(name);

        if(station == null) {
            return BadRequest($"Sorry couldn't find station {name}");
        }

        return Ok(station);
    }
}
