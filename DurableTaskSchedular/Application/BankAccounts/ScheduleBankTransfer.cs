using Application.Abstractions;
using Domain.Abstractions;
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

        public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            if (command.Amount <= 0)
                return BankAccountFailures.InvalidTransactionAmount(command.Amount);

            var fromBankAccountId = new BankAccountId(command.FromBankAccountId);
            var fromBankAccount = await _bankAccountRepository.GetByIdAsync(
                fromBankAccountId,
                cancellationToken
            );

            if (fromBankAccount is null)
                return BankAccountFailures.UnableToFindBankAccount(fromBankAccountId);

            var toBankAccountId = new BankAccountId(command.ToBankAccountId);
            var toBankAccount = await _bankAccountRepository.GetByIdAsync(
                toBankAccountId,
                cancellationToken
            );

            if (toBankAccount is null)
                return BankAccountFailures.UnableToFindBankAccount(toBankAccountId);

            var result = fromBankAccount.ScheduleTransfer(toBankAccount, command.Amount, command.ScheduleDateTime);
            if (result is not SuccessfulResult)
                return result;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return SuccessfulResult.Created();
        }
    }
}
