namespace Waterloo.Model;

public record AffectedUser(
    Guid Id,
    Guid UserId,
    Guid DisruptionId,
    Station StartStation, 
    Station EndStation, 
    IEnumerable<Station> AffectedStations,
    TimeOnly EndTime)
{
    public Serverity Severity { get; init; }
    public int DisruptionSpanLength { get; init; }
    public int TotalJourneyStations { get; init; }
    public bool IsFullJourneyCoverage =>
        AffectedStations.Count() == TotalJourneyStations;
    public int OverlapCount =>
        AffectedStations.Count();
}