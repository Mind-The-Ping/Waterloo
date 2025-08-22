namespace Waterloo.Model;

public record JourneyReturn(
    Guid StartStationId, 
    Guid EndStationId, 
    TimeOnly StartTime,
    TimeOnly EndTime,
    IList<DayOfWeek> DaysToCheck,
    Serverity Serverity);