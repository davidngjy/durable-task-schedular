using Domain.Users;

namespace Domain.BankAccounts;

public interface IBankAccountRepository
{
    void Add(BankAccount bankAccount);

    Task<IReadOnlyCollection<BankAccount>> GetAllAsync(CancellationToken cancellationToken);

    Task<BankAccount?> GetByIdWithUserIdAsync(UserId userId, BankAccountId id, CancellationToken cancellationToken);

    Task<BankAccount?> GetByIdAsync(BankAccountId id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<BankAccount>> GetWithReadyForTransferAsync(CancellationToken cancellationToken);
}
