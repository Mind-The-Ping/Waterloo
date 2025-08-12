namespace Waterloo.Repository.Station;
public class Station
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public IEnumerable<Guid>? LineIds { get; set; }
}
