using System.Text.Json.Serialization;

namespace Waterloo.Repository.Route;
public class LineData : Dictionary<Guid, LineDto> { }

public class LineDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("validRoutes")]
    public required List<ValidRouteDto> ValidRoutes { get; set; }
}

public class ValidRouteDto
{
    [JsonPropertyName("from")]
    public required StationDto From { get; set; }

    [JsonPropertyName("to")]
    public required StationDto To { get; set; }

    [JsonPropertyName("stations")]
    public required List<StationDto> Stations { get; set; }
}

public class StationDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
