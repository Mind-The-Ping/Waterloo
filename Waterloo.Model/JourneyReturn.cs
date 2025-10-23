namespace Waterloo.Model;

public record JourneyReturn(
    Guid Id,
    Line Line,
    Station StartStation, 
    Station EndStation, 
    TimeOnly StartTime,
    TimeOnly EndTime,
    IList<DayOfWeek> DaysToCheck,
    Serverity Serverity);