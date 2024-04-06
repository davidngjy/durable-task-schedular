using Domain.Users;
using User = Application.ReadModels.User;

namespace Application.Users;

public static class GetUsers
{
    public class Handler
    {
        private readonly IUserRepository _userRepository;

        public Handler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> HandleAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);

            return users.Select(u => new User
            {
                Id = u.Id.Value,
                FirstName = u.Name.FirstName,
                LastName = u.Name.LastName
            });
        }
    }
}
