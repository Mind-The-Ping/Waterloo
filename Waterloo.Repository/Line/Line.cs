using System.Text.Json.Serialization;

namespace Waterloo.Repository.Line;
public class Line
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
