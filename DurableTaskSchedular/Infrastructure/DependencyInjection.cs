using Application.Abstractions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("in-memory-db");
            });

        serviceCollection.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return serviceCollection;
    }
}
