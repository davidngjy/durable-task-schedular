using Domain.Abstractions;
using Domain.BankAccounts;
using Domain.Users;

namespace Domain.ScheduledBankAccountCreations;

public class ScheduledBankAccountCreation : IAggregateRoot
{
    public ScheduledBankAccountCreationId Id { get; }

    public Currency Currency { get; }

    public DateTimeOffset ScheduleDateTime { get; }

    public UserId UserId { get; }

    private ScheduledBankAccountCreation()
    {
        // For EF Core
        Id = default!;
        Currency = default!;
        ScheduleDateTime = default!;
        UserId = default!;
    }

    public ScheduledBankAccountCreation(DateTimeOffset scheduleDateTime, User user, Currency currency)
    {
        ScheduleDateTime = scheduleDateTime;
        Currency = currency;
        Id = new ScheduledBankAccountCreationId(Guid.NewGuid());
        UserId = user.Id;
    }
}
