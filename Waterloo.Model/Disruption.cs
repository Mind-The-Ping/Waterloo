namespace Waterloo.Model;

public readonly struct Disruption(
    Guid id,
    Line line,
    Station startStation,
    Station endStation,
    Station originalStartStation,
    Station originalEndStation,
    string description,
    Serverity severity,
    Guid severityId,
    Guid descriptionId,
    DateTime lastUpdatedUtc)
{
    public Guid Id { init; get; } = id;
    public Line Line { init; get; } = line;
    public Station StartStation { init; get; } = startStation;
    public Station EndStation { init; get; } = endStation;
    public Station OriginalStartStation { init; get; } = originalStartStation;
    public Station OriginalEndStation { init; get; } = originalEndStation;
    public string Description { init; get; } = description;
    public Serverity Severity { init; get; } = severity;
    public Guid SeverityId { init; get; } = severityId;
    public Guid DescriptionId { init; get; } = descriptionId;
    public DateTime LastUpdatedUtc { init; get; } = lastUpdatedUtc;
    public string Key =>
        $"{Line.Id}-{StartStation.Id}-{EndStation.Id}";
}
