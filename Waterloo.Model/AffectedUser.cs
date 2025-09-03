namespace Waterloo.Model;
public record AffectedUser(
    Guid Id, 
    Station StartStation, 
    Station EndStation, 
    TimeOnly EndTime);