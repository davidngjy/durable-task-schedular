using Application;
using Infrastructure;

namespace DurableTaskSchedular.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSwaggerGen()
            .AddEndpointsApiExplorer();

        serviceCollection
            .AddApplicationServices()
            .AddInfrastructureServices();

        return serviceCollection;
    }
}
