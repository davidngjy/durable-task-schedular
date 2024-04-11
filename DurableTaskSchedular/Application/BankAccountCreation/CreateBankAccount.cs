using Application.Abstractions;
using Domain.BankAccounts;
using Domain.Users;

namespace Application.BankAccountCreation;

public static class CreateBankAccount
{
    public record Command
    {
        public required Guid UserId { get; init; }

        public required string Currency { get; init; }
    }

    public class Handler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IBankAccountRepository _bankAccountRepository;

        public Handler(IUnitOfWork unitOfWork, IUserRepository userRepository,
            IBankAccountRepository bankAccountRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<BankAccountId> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            var currency = Currency.TryFromCode(command.Currency);
            if (currency is null)
                throw new Exception($"Currency {command.Currency} is not supported.");

            var user = await _userRepository.GetByIdAsync(new UserId(command.UserId), cancellationToken);
            if (user is null)
                throw new Exception($"User Id {command.UserId} not found.");

            var newBankAccount = new BankAccount(
                user.Id,
                currency);

            _bankAccountRepository.Add(newBankAccount);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newBankAccount.Id;
        }
    }
}
