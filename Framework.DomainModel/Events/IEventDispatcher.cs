namespace Sharpee.Framework.DomainModel.Events;

/// <summary>
/// Interface that should be implemented by every dispatcher class.
/// </summary>
public interface IEventDispatcher
{
    void Dispatch<TEvent>(params TEvent[] events) where TEvent : IEvent;
}