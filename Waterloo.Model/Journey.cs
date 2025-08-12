namespace Waterloo.Model;

public enum Serverity
{
    Good = 0,
    Minor = 1,
    Severe = 2,
    Suspended = 3,
    Closed = 4
}

public class Journey
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid LineId { get; set; }
    public required IList<Guid> StationIds { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public required IList<DayOfWeek> DaysToCheck { get; set; }
    public required Serverity Serverity { get; set; }
}
