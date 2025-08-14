using System.Text.Json;

namespace Waterloo.Repository.Station;
public class StationRepository
{
    public Station[] Stations { get; init; }

    public StationRepository()
    {
        using StreamReader r = new(Path.Combine(AppContext.BaseDirectory, "Data", "station.json"));
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
       var station = Stations.FirstOrDefault(x => StationNameMatch(name, x.Name));
        return station == null ?
            null : new Model.Station(station.Id, station.Name);
    }

    public Model.Station? GetStationById(Guid id)
    {
        var station = Stations.FirstOrDefault(x => x.Id == id);
        return station == null ?
            null : new Model.Station(station.Id, station.Name);
    }

    private static bool StationNameMatch(string query, string stationName)
    {
        query = NormalizeString(query);
        stationName = NormalizeString(stationName);

        return query.Contains(stationName);
    }

    private static string NormalizeString(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) {
            return string.Empty;
        }

        return new string(input
            .Where(char.IsLetterOrDigit)
            .ToArray())
            .ToLowerInvariant()
            .Trim();
    }
}
