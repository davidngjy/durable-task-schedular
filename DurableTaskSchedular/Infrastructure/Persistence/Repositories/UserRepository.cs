using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(User user) => _dbContext.Users.Add(user);

    public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken)
        => await _dbContext.Users.ToListAsync(cancellationToken);

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken)
        => await _dbContext
            .Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
}
