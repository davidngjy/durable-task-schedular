using Application.Abstractions;
using Domain.BankAccounts;

namespace Application.BankAccounts;

public static class ScheduleBankTransfer
{
    public record Command
    {
        public required Guid FromBankAccountId { get; init; }

        public required Guid ToBankAccountId { get; init; }

        public required DateTimeOffset ScheduleDateTime { get; init; }

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
                throw new Exception("Transfer amount must be greater than 0");

            var fromBankAccount = await _bankAccountRepository.GetByIdAsync(
                new BankAccountId(command.FromBankAccountId),
                cancellationToken);

            if (fromBankAccount is null)
                throw new Exception(
                    $"Unable to find bank account with BankAccountId {command.FromBankAccountId}");

            var toBankAccount = await _bankAccountRepository.GetByIdAsync(
                new BankAccountId(command.ToBankAccountId),
                cancellationToken);

            if (toBankAccount is null)
                throw new Exception($"Unable to find bank account {command.ToBankAccountId}");

            fromBankAccount.ScheduleTransfer(toBankAccount, command.Amount, command.ScheduleDateTime);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
