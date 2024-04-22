using Application;
using DurableTaskSchedular.Web.BackgroundServices;
using Infrastructure;

namespace DurableTaskSchedular.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSwaggerGen()
            .AddEndpointsApiExplorer()
            .AddProblemDetails()
            .AddHttpContextAccessor();

        serviceCollection
            .AddApplicationServices()
            .AddInfrastructureServices();

        serviceCollection
            .AddHostedService<ScheduledBankAccountCreationProcessor>()
            .AddHostedService<ScheduledBankTransferProcessor>();

        return serviceCollection;
    }
}
