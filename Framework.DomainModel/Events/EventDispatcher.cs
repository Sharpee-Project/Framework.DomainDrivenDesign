using Sharpee.Framework.DomainModel.Exceptions;
using System;
using System.Linq;
using System.Reflection;

namespace Sharpee.Framework.DomainModel.Events;

/// <summary>
/// It can dispatch all events of an aggregate root.
/// </summary>
public sealed class EventDispatcher : IEventDispatcher
{
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
            if (@event == null)
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
                    var instance = Activator.CreateInstance(handler);
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
}