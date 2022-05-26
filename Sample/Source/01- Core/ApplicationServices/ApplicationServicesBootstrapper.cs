using Microsoft.Extensions.DependencyInjection;

namespace Sample.Core.ApplicationServices;

public static class ApplicationServicesBootstrapper
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services;
    }
}
