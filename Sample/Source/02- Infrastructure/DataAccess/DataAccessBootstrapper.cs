using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Infrastructure.DataAccess;

public static class DataAccessBootstrapper
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseDataAccess(this IApplicationBuilder app)
    {
        return app;
    }
}
