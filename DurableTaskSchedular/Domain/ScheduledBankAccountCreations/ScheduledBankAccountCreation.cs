using Domain.Abstractions;
using Domain.Users;

namespace Domain.ScheduledBankAccountCreations;

public class ScheduledBankAccountCreation : IAggregateRoot
{
    public ScheduledBankAccountCreationId Id { get; }

    public DateTimeOffset ScheduleDateTime { get; }

    public UserId OwnerId { get; }

    public ScheduledBankAccountCreation(DateTimeOffset scheduleDateTime, User user)
    {
        ScheduleDateTime = scheduleDateTime;
        Id = new ScheduledBankAccountCreationId(Guid.NewGuid());
        OwnerId = user.Id;
    }
}
