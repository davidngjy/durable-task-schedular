namespace Application.ReadModels;

public class ScheduledBankAccountCreation
{
    public required Guid Id { get; init; }

    public required Guid UserId { get; init; }

    public required string Currency { get; init; }

    public required DateTimeOffset ScheduledDateTime { get; init; }
}
