namespace Domain.BankAccounts;

public class ScheduledTransfer
{
    public ScheduledTransferId Id { get; } = new(Guid.NewGuid());

    public required BankAccountId To { get; init; }

    public required decimal Amount { get; init; }

    public required DateTimeOffset ScheduledDateTime { get; init; }
}
