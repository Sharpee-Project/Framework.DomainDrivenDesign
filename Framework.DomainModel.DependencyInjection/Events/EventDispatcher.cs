using Microsoft.Extensions.DependencyInjection;
using Sharpee.Framework.DomainModel.Events;
using Sharpee.Framework.DomainModel.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sharpee.Framework.DomainModel.DependencyInjection.Events;

/// <summary>
/// It can dispatch all events of an aggregate root.
/// </summary>
public sealed class EventDispatcher : IEventDispatcher
{
    private readonly ServiceProvider _provider;

    public EventDispatcher(ServiceProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Dispatches all events of an aggregate root.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <param name="events"></param>
    public void Dispatch<TEvent>(params TEvent[] events)
        where TEvent : IEvent
    {
        var handlers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(c =>
            c.GetTypes().Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>))));

        foreach (var @event in events)
        {
            if (@event is null)
            {
                throw new SharpeeException(logMessage: "Event can NOT be null");
            }

            foreach (var handler in handlers)
            {
                var canHandleEvent = handler.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IEventHandler<>) &&
                        i.GenericTypeArguments[0] == @event.GetType());

                if (canHandleEvent)
                {
                    var @params = InitializeDependencies(handler, _provider);
                    var instance = Activator.CreateInstance(handler, @params);
                    var method = instance
                        .GetType()
                        .GetRuntimeMethods()
                        .First(m => m.Name.Equals(nameof(IEventHandler<IEvent>.Handle)));
                    var parametersArray = new object[] { @event };
                    method.Invoke(instance, parametersArray);
                }
            }
        }
    }

    private object[] InitializeDependencies(Type type, ServiceProvider provider)
    {
        var result = new List<object>();

        var parameters = type.GetConstructors()[0].GetParameters();
        foreach (var param in parameters)
        {
            result.Add(_provider.GetRequiredService(param.ParameterType));
        }

        return result.ToArray();
    }
}