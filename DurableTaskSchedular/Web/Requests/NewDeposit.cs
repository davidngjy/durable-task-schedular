using System.Text.Json.Serialization;

namespace DurableTaskSchedular.Web.Requests;

public record NewDeposit
{
    [JsonPropertyName("amount")] public required decimal Amount { get; init; }
}
