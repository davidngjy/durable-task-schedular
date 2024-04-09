using System.Text.Json.Serialization;

namespace DurableTaskSchedular.Web.Requests;

public record CreateNewUser
{
    [JsonPropertyName("first_name")] public required string FirstName { get; init; }

    [JsonPropertyName("last_name")] public required string LastName { get; init; }
}
