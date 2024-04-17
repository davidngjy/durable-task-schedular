using Application.BankAccountCreation;
using Application.BankAccounts;
using Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddTransient<CreateBankAccount.Handler>()
            .AddTransient<ExecuteReadyForCreationBankAccounts.Handler>()
            .AddTransient<GetScheduledBankAccountCreations.Handler>()
            .AddTransient<ScheduleBankAccountCreation.Handler>();

        serviceCollection
            .AddTransient<DepositMoney.Handler>()
            .AddTransient<ExecuteReadyForBankTransfer.Handler>()
            .AddTransient<GetBankAccounts.Handler>()
            .AddTransient<ScheduleBankTransfer.Handler>()
            .AddTransient<WithdrawMoney.Handler>();

        serviceCollection
            .AddTransient<CreateUser.Handler>()
            .AddTransient<GetUsers.Handler>();

        return serviceCollection;
    }
}
