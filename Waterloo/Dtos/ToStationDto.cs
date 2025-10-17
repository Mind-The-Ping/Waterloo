namespace Waterloo.Dtos;

public record ToStationDto(
    Guid LineId, 
    Guid FromStationId);
