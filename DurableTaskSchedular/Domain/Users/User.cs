using Domain.Abstractions;
using Domain.BankAccounts;

namespace Domain.Users;

public class User : IAggregateRoot
{
    public UserId Id { get; }

    public Name Name { get; }

    private readonly List<BankAccountId> _bankAccountIds = [];
    public IReadOnlyCollection<BankAccountId> BankAccountIds => _bankAccountIds.AsReadOnly();

    public User(Name name)
    {
        Id = new UserId(Guid.NewGuid());
        Name = name;
    }

    public void AddBankAccount(BankAccount bankAccount) => _bankAccountIds.Add(bankAccount.Id);
}
