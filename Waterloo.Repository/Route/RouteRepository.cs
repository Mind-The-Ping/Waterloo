using System.Text.Json;

namespace Waterloo.Repository.Route;

public class RouteRepository
{
    public LineData Lines { get; set; }

    public RouteRepository()
    {
        using StreamReader r = new("route.json");
        string json = r.ReadToEnd();
        Lines = JsonSerializer.Deserialize<LineData>(json)
           ?? throw new Exception("route.json file not found");
    }

    public IEnumerable<Model.Station> GetStationsBetween(Guid lineId, Guid start, Guid end)
    {
        if (!Lines.TryGetValue(lineId, out var line) || line == null)
            return Enumerable.Empty<Model.Station>();

        foreach (var validRoute in line.ValidRoutes)
        {
            int startIndex = validRoute.Stations.FindIndex(x => x.Id == start);
            int endIndex = validRoute.Stations.FindIndex(x => x.Id == end);

            if (startIndex == -1 || endIndex == -1)
                continue;

            if (startIndex <= endIndex)
            {
                var result = validRoute.Stations.GetRange(startIndex, endIndex - startIndex + 1)
                    .Select(s => new Model.Station(s.Id, s.Name));
                return result;
            }
            else
            {
                var reversedResult = validRoute.Stations
                    .GetRange(endIndex, startIndex - endIndex + 1)
                    .Select(s => new Model.Station(s.Id, s.Name))
                    .Reverse();
                return reversedResult;
            }
        }

        return Enumerable.Empty<Model.Station>();
    }

    public IEnumerable<Model.Station> GetRoute(Guid lineId, Guid start, Guid end)
    {
        if (!Lines.TryGetValue(lineId, out var line) || line == null)
            return Enumerable.Empty<Model.Station>();

        foreach (var validRoute in line.ValidRoutes)
        {
            int startIndex = validRoute.Stations.FindIndex(x => x.Id == start);
            int endIndex = validRoute.Stations.FindIndex(x => x.Id == end);

            if (startIndex == -1 || endIndex == -1)
                continue;

            if (startIndex <= endIndex)
            {
                var result = validRoute.Stations
                    .Select(s => new Model.Station(s.Id, s.Name));
                return result;
            }
            else
            {
                var reversedResult = validRoute.Stations
                    .Select(s => new Model.Station(s.Id, s.Name))
                    .Reverse();
                return reversedResult;
            }
        }

        return Enumerable.Empty<Model.Station>();
    }
}
