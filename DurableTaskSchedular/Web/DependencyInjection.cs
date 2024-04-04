using Application;
using Infrastructure;

namespace DurableTaskSchedular.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddCoreServices()
            .AddInfrastructureServices();

        return serviceCollection;
    }
}
