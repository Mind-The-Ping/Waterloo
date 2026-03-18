using CSharpFunctionalExtensions;
using System.Text.Json;
using Waterloo.Repository.Route;

namespace Waterloo.Repository.Station;
public class StationRepository
{
    private static readonly string[] _suffixesToRemove =
    [
        "undergroundstation",
        "railstation",
        "dlrstation",
        "tramstop",
        "station"
    ];

    private static Dictionary<string, string[]> _stationAliases;

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

        using StreamReader r3 = new(Path.Combine(AppContext.BaseDirectory, "Data", "alias.json"));
        json = r3.ReadToEnd();
        _stationAliases = JsonSerializer.Deserialize<Dictionary<string, string[]>>(json)
           ?? throw new Exception("alias.json file not found");
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
        var normalizedQuery = NormalizeStringWithSuffixes(name);

        var result = FindStationByName(normalizedQuery);

        if(result != null) {
            return result;
        }

        foreach (var aliasDic in _stationAliases)
        {
            var actualName = NormalizeString(aliasDic.Key);
            var aliases = aliasDic.Value;

            foreach (var alias in aliases)
            {
                if (NormalizeString(alias) == normalizedQuery) {
                    return FindStationByName(actualName);
                }
            }
        }

        return null;
    }

    public Model.Station? GetStationById(Guid id)
    {
        var station = Stations.FirstOrDefault(x => x.Id == id);
        return station == null ?
            null : new Model.Station(station.Id, station.Name);
    }

    public Result<IEnumerable<Model.Station>> GetStationsById(IEnumerable<Guid> stationIds)
    {
        var stations = new HashSet<Model.Station>();

        foreach(var stationId in stationIds)
        {
            var station = GetStationById(stationId);
            if (station == null) {
                
                return Result.Failure<IEnumerable<Model.Station>>
                    ($"Could not find station with id: {stationId}");
            }

            stations.Add(station);
        }

        return Result.Success<IEnumerable<Model.Station>>(stations);
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

    private Model.Station? FindStationByName(string name)
    {
        var station = Stations.FirstOrDefault(x => NormalizeString(x.Name) == name);
        return station == null ? null : new Model.Station(station.Id, station.Name);
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

    private static string NormalizeStringWithSuffixes(string input)
    {
        var normalized = NormalizeString(input);

        foreach (var suffix in _suffixesToRemove)
        {
            if (normalized.EndsWith(suffix))
            {
                normalized = normalized[..^suffix.Length];
                break;
            }
        }

        return normalized;
    }
}
