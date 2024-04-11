using Application.Abstractions;
using Domain.BankAccounts;
using Domain.ScheduledBankAccountCreations;
using Domain.Users;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            .AddDbContext<ApplicationDbContext>((provider, opt) =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var postgresConnectionString = configuration.GetConnectionString("Postgresql");
                opt.UseNpgsql(postgresConnectionString);
            });

        serviceCollection.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return serviceCollection;
    }
}
