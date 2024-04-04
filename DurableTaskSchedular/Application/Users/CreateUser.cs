using Application.Abstractions;
using Domain.Users;

namespace Application.Users;

public static class CreateUser
{
    public record Command
    {
        public required string FirstName { get; init; }

        public required string LastName { get; init; }
    }

    public class Handler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public Handler(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<UserId> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            var newUser = new User(new Name(command.FirstName, command.LastName));

            _userRepository.Add(newUser);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newUser.Id;
        }
    }
}
