namespace Waterloo.Dtos;
public record AffectedJourneysDto(
    Guid LineId,
    TimeOnly QueryTime,
    DayOfWeek QueryDay,
    IEnumerable<DisruptionDto> Disruptions);
