using System.Text.Json;

namespace Waterloo.Repository.Station;
public class StationRepository
{
    public Station[] Stations { get; init; }

    public StationRepository()
    {
        using StreamReader r = new("station.json");
        string json = r.ReadToEnd();
        Stations = JsonSerializer.Deserialize<Station[]>(json)
            ?? throw new Exception("station.json file not found");
    }

    public IEnumerable<Model.Station> GetByLine(Guid id) 
    {
       return Stations.Where(
           x => x.LineIds != null && 
           x.LineIds.Contains(id))
            .Select(x => new Model.Station(x.Id, x.Name));
    }

    public Model.Station? GetStationByName(string name)
    {
       var station = Stations.FirstOrDefault(x => AreStringEqual(x.Name, name));
        return station == null ?
            null : new Model.Station(station.Id, station.Name);
    }

    public Model.Station? GetStationById(Guid id)
    {
        var station = Stations.FirstOrDefault(x => x.Id == id);
        return station == null ?
            null : new Model.Station(station.Id, station.Name);
    }

    private static bool AreStringEqual(string a, string b)
    {
        a = a.ToLowerInvariant();
        b = b.ToLowerInvariant();

        a = string.Concat(a.Where(c => !char.IsWhiteSpace(c)));
        b = string.Concat(b.Where(c => !char.IsWhiteSpace(c)));

        return string.Equals(a, b, StringComparison.Ordinal);
    }
}
