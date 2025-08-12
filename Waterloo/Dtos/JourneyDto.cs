using Waterloo.Model;

namespace Waterloo.Dtos;

public record JourneyDto(
    Guid LineId, 
    Guid StartStationId, 
    Guid EndStationId,
    TimeOnly StartTime,
    TimeOnly EndTime,
    IEnumerable<DayOfWeek> DaysToCheck,
    Serverity Serverity);
