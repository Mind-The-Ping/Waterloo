namespace Waterloo.Model;

public record AffectedUserDto(
    Guid Id,
    Guid UserId,
    Guid DisruptionId,
    Station StartStation,
    Station EndStation,
    IEnumerable<Station> AffectedStations,
    TimeOnly EndTime);
