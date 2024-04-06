namespace Domain.ScheduledBankAccountCreations;

public interface IScheduledBankAccountCreationRepository
{
    void Add(ScheduledBankAccountCreation scheduledBankAccountCreation);

    void Remove(ScheduledBankAccountCreation scheduledBankAccountCreation);

    Task<IReadOnlyCollection<ScheduledBankAccountCreation>> GetAllAsync(CancellationToken cancellationToken);

    Task<IReadOnlyCollection<ScheduledBankAccountCreation>> GetReadyForCreationAsync(CancellationToken cancellationToken);
}
