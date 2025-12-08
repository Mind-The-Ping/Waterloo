namespace Waterloo.Model;

public record Disruption(
    Guid Id,
    Guid StartStationId,
    Guid EndStationId,
    Serverity Serverity);
