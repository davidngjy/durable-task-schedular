using Domain.BankAccounts;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BankAccountRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(BankAccount bankAccount) => _dbContext.BankAccounts.Add(bankAccount);

    public async Task<IReadOnlyCollection<BankAccount>> GetAllAsync(CancellationToken cancellationToken)
        => await _dbContext.BankAccounts.ToListAsync(cancellationToken);

    public async Task<BankAccount?> GetByIdWithOwnerIdAsync(UserId ownerId, BankAccountId id, CancellationToken cancellationToken)
        => await _dbContext
            .BankAccounts
            .Where(a => a.Id == id && a.OwnerId == ownerId)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<BankAccount?> GetByIdAsync(BankAccountId id, CancellationToken cancellationToken)
        => await _dbContext
            .BankAccounts
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyCollection<BankAccount>> GetWithReadyForTransferAsync(CancellationToken cancellationToken)
        => await _dbContext
            .BankAccounts
            .Where(a => a.ScheduledTransfers.Any(t => t.ScheduledDateTime <= DateTimeOffset.Now))
            .ToListAsync(cancellationToken);
}
