namespace Application.ReadModels;

public record User
{
    public required Guid Id { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }
}
