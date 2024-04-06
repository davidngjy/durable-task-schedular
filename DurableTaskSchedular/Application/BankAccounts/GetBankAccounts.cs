using Domain.BankAccounts;

namespace Application.BankAccounts;

public static class GetBankAccounts
{
    public class Handler
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public Handler(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<IEnumerable<ReadModels.BankAccount>> HandleAsync(CancellationToken cancellationToken)
        {
            var bankAccounts = await _bankAccountRepository.GetAllAsync(cancellationToken);

            return bankAccounts.Select(a => new ReadModels.BankAccount
            {
                Id = a.Id.Value,
                UserId = a.OwnerId.Value,
                Currency = a.Currency.Code,
                Balance = a.Balance.Amount,
                CreatedDateTime = a.CreatedDateTime,
                ScheduledTransfers = a.ScheduledTransfers.Select(t => new ReadModels.ScheduledTransfer
                {
                    Id = t.Id.Value,
                    ToBankAccountId = t.To.Value,
                    Amount = t.Amount,
                    ScheduledDateTime = t.ScheduledDateTime
                }).ToList()
            });
        }
    }
}
