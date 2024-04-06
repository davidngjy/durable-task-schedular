using Application.Abstractions;
using Domain.BankAccounts;
using Domain.ScheduledBankAccountCreations;
using Domain.Users;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IBankAccountRepository, BankAccountRepository>()
            .AddScoped<IScheduledBankAccountCreationRepository, ScheduledBankAccountCreationRepository>()
            .AddScoped<IUserRepository, UserRepository>();

        serviceCollection
            .AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("in-memory-db");
            });

        serviceCollection.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return serviceCollection;
    }
}
