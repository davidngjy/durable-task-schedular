using Domain.ScheduledBankAccountCreations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ScheduledBankAccountCreationRepository : IScheduledBankAccountCreationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ScheduledBankAccountCreationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(ScheduledBankAccountCreation scheduledBankAccountCreation)
        => _dbContext.ScheduledBankAccountCreations.Add(scheduledBankAccountCreation);

    public void Remove(ScheduledBankAccountCreation scheduledBankAccountCreation)
        => _dbContext.ScheduledBankAccountCreations.Remove(scheduledBankAccountCreation);

    public async Task<IReadOnlyCollection<ScheduledBankAccountCreation>> GetAllAsync(CancellationToken cancellationToken)
        => await _dbContext.ScheduledBankAccountCreations.ToListAsync(cancellationToken);

    public async Task<IReadOnlyCollection<ScheduledBankAccountCreation>> GetReadyForCreationAsync(CancellationToken cancellationToken)
        => await _dbContext
            .ScheduledBankAccountCreations
            .Where(c => c.ScheduleDateTime <= DateTimeOffset.UtcNow)
            .ToListAsync(cancellationToken);
}
