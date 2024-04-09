using Domain.Abstractions;

namespace Domain.Users;

public class User : IAggregateRoot
{
    public UserId Id { get; }

    public Name Name { get; }

    private User()
    {
        Id = default!;
        Name = default!;
    }

    public User(Name name)
    {
        Id = new UserId(Guid.NewGuid());
        Name = name;
    }
}
