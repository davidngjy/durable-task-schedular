namespace Domain.Users;

public interface IUserRepository
{
    void Add(User user);

    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken);
}
