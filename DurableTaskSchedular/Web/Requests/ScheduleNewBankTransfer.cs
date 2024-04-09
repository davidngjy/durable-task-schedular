using System.Text.Json.Serialization;

namespace DurableTaskSchedular.Web.Requests;

public record ScheduleNewBankTransfer
{
    [JsonPropertyName("to_bank_account_id")]
    public required Guid ToBankAccountId { get; init; }

    [JsonPropertyName("amount")] public required decimal Amount { get; init; }

    [JsonPropertyName("schedule_datetime")]
    public required DateTimeOffset ScheduleDateTime { get; init; }
}
