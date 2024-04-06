namespace Application.ReadModels;

public record BankAccount
{
    public required Guid Id { get; init; }

    public required Guid UserId { get; init; }

    public required string Currency { get; init; }

    public required DateTimeOffset CreatedDateTime { get; init; }

    public required decimal Balance { get; init; }

    public required IReadOnlyCollection<ScheduledTransfer> ScheduledTransfers { get; init; }
}
