namespace Domain.BankAccounts;

public interface IBankAccountRepository
{
    void Add(BankAccount bankAccount);

    Task<BankAccount?> GetById(BankAccountId id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<BankAccount>> GetWithReadyForTransferAsync(CancellationToken cancellationToken);
}
