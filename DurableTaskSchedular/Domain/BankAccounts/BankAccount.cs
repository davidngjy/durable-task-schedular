using Domain.Abstractions;
using Domain.Users;

namespace Domain.BankAccounts;

public class BankAccount : IAggregateRoot
{
    public BankAccountId Id { get; }

    public UserId OwnerId { get; }

    public Currency Currency { get; }

    public DateTimeOffset CreatedDateTime { get; }

    public Balance Balance { get; private set; }

    private readonly List<ScheduledTransfer> _scheduledTransfers = [];
    public IReadOnlyCollection<ScheduledTransfer> ScheduledTransfers => _scheduledTransfers.AsReadOnly();

    public BankAccount(UserId ownerId, Currency currency)
    {
        Id = new BankAccountId(Guid.NewGuid());
        OwnerId = ownerId;
        Currency = currency;
        Balance = new Balance(0);
        CreatedDateTime = DateTimeOffset.Now;
    }

    public void Deposit(decimal amount) => Balance = new Balance(Balance.Amount + amount);

    public void Withdraw(decimal amount) => Balance = new Balance(Balance.Amount - amount);

    public void ScheduleTransfer(BankAccount to, decimal amount, DateTimeOffset scheduleDateTime)
    {
        var availableBalance = GetCurrentAvailableBalance();
        if (availableBalance.Amount - amount < 0)
            throw new Exception("Insufficient balance");

        _scheduledTransfers.Add(new ScheduledTransfer
        {
            To = to.Id,
            ScheduledDateTime = scheduleDateTime.ToUniversalTime(),
            Amount = amount
        });
    }

    public async Task ProcessScheduledTransferAsync(IBankAccountRepository bankAccountRepository,
        CancellationToken cancellationToken)
    {
        foreach (var transfer in _scheduledTransfers.Where(t => t.ScheduledDateTime <= DateTimeOffset.Now).ToList())
        {
            var toBankAccount = await bankAccountRepository.GetByIdAsync(transfer.To, cancellationToken);
            if (toBankAccount is null)
                throw new Exception("Bank Account not found");

            Withdraw(transfer.Amount);
            toBankAccount.Deposit(transfer.Amount);

            _scheduledTransfers.RemoveAll(t => t.Id == transfer.Id);
        }
    }

    private Balance GetCurrentAvailableBalance()
    {
        var availableBalance = _scheduledTransfers
            .Aggregate(
                0m,
                (acc, t) => acc + t.Amount);

        return new Balance(availableBalance);
    }
}
