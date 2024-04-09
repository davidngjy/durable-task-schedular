using System.Text.Json.Serialization;

namespace DurableTaskSchedular.Web.Requests;

public record ScheduleCreateNewBankAccount
{
    [JsonPropertyName("user_id")] public required Guid UserId { get; init; }

    [JsonPropertyName("currency")] public required string Currency { get; init; }

    [JsonPropertyName("schedule_datetime")]
    public required DateTimeOffset ScheduleDateTime { get; init; }
}
