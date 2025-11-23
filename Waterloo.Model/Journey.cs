using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Waterloo.Model;

public enum Serverity
{
    Minor = 0,
    Severe = 1,
    Closed = 2
}

public class Journey
{
    [property: BsonId]
    [property: BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    [BsonRepresentation(BsonType.String)]
    public Guid UserId { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid LineId { get; set; }
    [BsonRepresentation(BsonType.String)]
    public required IList<Guid> StationIds { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public required IList<DayOfWeek> DaysToCheck { get; set; }
    public required Serverity Serverity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; } = null!;
}
