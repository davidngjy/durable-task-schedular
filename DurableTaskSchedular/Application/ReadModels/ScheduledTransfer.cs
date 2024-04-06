namespace Application.ReadModels;

public record ScheduledTransfer
{
    public required Guid Id { get; init; }

    public required Guid ToBankAccountId { get; init; }

    public required decimal Amount { get; init; }

    public required DateTimeOffset ScheduledDateTime { get; init; }
}
