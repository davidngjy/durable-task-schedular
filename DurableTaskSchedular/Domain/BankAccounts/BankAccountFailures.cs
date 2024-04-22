using System.Globalization;
using Domain.Abstractions;

namespace Domain.BankAccounts;

public static class BankAccountFailures
{
    public static FailureResult InsufficientAvailableBalance(decimal balanceAfter, decimal amount)
        => FailureResult.Validation(
            "Insufficient Available Balance",
            "BankAccount.InsufficientAvailableBalance",
            "The bank account does not have sufficient available balance to perform the requested operation",
            new Dictionary<string, string>
            {
                { "balance_after", balanceAfter.ToString(CultureInfo.InvariantCulture) },
                { "transaction_amount", amount.ToString(CultureInfo.InvariantCulture) }
            });

    public static FailureResult UnableToFindBankAccount(BankAccountId id)
        => FailureResult.NotFound(
            "Unable to find bank account",
            "BankAccount.NotFound",
            "The bank account is not found.",
            new Dictionary<string, string>
            {
                { "bank_account_id", id.Value.ToString() }
            });

    public static FailureResult InvalidTransactionAmount(decimal amount)
        => FailureResult.Validation(
            "Invalid transaction amount",
            "BankAccount.InvalidTransactionAmount",
            "The request amount is invalid.",
            new Dictionary<string, string>
            {
                { "transaction_amount", amount.ToString(CultureInfo.InvariantCulture) }
            });
}
