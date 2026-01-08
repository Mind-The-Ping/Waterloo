using System.Text.Json.Serialization;

namespace Waterloo.Consumer;
public record DeletePremiumJourneysMessage(
    [property: JsonPropertyName("userId")]
    Guid UserId);
