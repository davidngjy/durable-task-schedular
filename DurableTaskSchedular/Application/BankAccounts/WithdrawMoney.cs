using Application.Abstractions;
using Domain.BankAccounts;
using Domain.Users;

namespace Application.BankAccounts;

public static class WithdrawMoney
{
    public record Command
    {
        public required Guid UserId { get; init; }

        public required Guid BankAccountId { get; init; }

        public required decimal Amount { get; init; }
    }

    public class Handler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAccountRepository _bankAccountRepository;

        public Handler(IUnitOfWork unitOfWork, IBankAccountRepository bankAccountRepository)
        {
            _unitOfWork = unitOfWork;
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task HandleAsync(Command command, CancellationToken cancellationToken)
        {
            if (command.Amount <= 0)
                throw new Exception("Withdraw amount must be greater than 0");

            var bankAccount = await _bankAccountRepository.GetByIdWithUserIdAsync(
                new UserId(command.UserId),
                new BankAccountId(command.BankAccountId),
                cancellationToken);

            if (bankAccount is null)
                throw new Exception($"Unable to find bank account {command.BankAccountId}");

            bankAccount.Withdraw(command.Amount);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
