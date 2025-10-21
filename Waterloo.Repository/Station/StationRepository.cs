using System.Text.Json;
using Waterloo.Repository.Route;

namespace Waterloo.Repository.Station;
public class StationRepository
{
    public Station[] Stations { get; init; }

    public LineData Lines { get; set; }

    public StationRepository()
    {
        using StreamReader r = new(Path.Combine(AppContext.BaseDirectory, "Data", "station.json"));
        string json = r.ReadToEnd();
        Stations = JsonSerializer.Deserialize<Station[]>(json)
            ?? throw new Exception("station.json file not found");

        using StreamReader r2 = new(Path.Combine(AppContext.BaseDirectory, "Data", "route.json"));
        json = r2.ReadToEnd();
        Lines = JsonSerializer.Deserialize<LineData>(json)
           ?? throw new Exception("route.json file not found");
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

    public IEnumerable<Model.Station> GetByToStation(Guid lineId, Guid fromStationId)
    {
        if (!Lines.TryGetValue(lineId, out var line) || line == null)
            return [];

        var validStations = new HashSet<Model.Station>();

        foreach (var validRoute in line.ValidRoutes)
        {
            if(validRoute.Stations.Any(x => x.Id == fromStationId)) 
            {
                foreach (var station in validRoute.Stations)
                {
                    if(station.Id == fromStationId) {
                        continue;
                    }

                    validStations.Add(new Model.Station(station.Id, station.Name));
                }
                
            }
        }

        return validStations
            .OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase);
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
