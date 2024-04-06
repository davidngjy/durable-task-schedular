namespace Domain.Users;

public interface IUserRepository
{
    void Add(User user);

    Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken);

    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken);
}