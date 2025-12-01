namespace Waterloo.Model;
public record AffectedUser(
    Guid Id,
    Guid UserId,
    Station StartStation, 
    Station EndStation, 
    IEnumerable<Station> AffectedStations,
    TimeOnly EndTime);