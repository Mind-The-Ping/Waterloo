using Waterloo.Model;

namespace Waterloo.Dtos;
public record AffectedJourneysDto(
    Guid LineId,
    Guid StartStationId,
    Guid EndStationId,
    TimeOnly QueryTime,
    DayOfWeek QueryDay,
    Serverity Serverity);
