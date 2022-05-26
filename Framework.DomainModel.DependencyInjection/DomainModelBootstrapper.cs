using Microsoft.Extensions.DependencyInjection;
using Sharpee.Framework.DomainModel.Events;

namespace Sharpee.Framework.DomainModel.DependencyInjection;

public static class DomainModelBootstrapper
{
    public static void AddSharpeeEventDispatcher(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        services.AddScoped<IEventDispatcher, Events.EventDispatcher>(p => new Events.EventDispatcher(provider));
    }
}