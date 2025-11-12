namespace Waterloo.Model;

public enum Serverity
{
    Minor = 0,
    Severe = 1,
    Closed = 2
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
    public DateTime CreatedAt { get; set; }
}
