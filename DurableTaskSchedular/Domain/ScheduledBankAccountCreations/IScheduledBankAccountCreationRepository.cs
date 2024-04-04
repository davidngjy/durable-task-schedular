namespace Domain.ScheduledBankAccountCreations;

public interface IScheduledBankAccountCreationRepository
{
    void Add(ScheduledBankAccountCreation scheduledBankAccountCreation);

    Task<IReadOnlyCollection<ScheduledBankAccountCreation>> GetReadyForCreationAsync(
        CancellationToken cancellationToken);
}
