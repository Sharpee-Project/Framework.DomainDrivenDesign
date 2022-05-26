namespace Sharpee.Framework.DomainModel.Events;

/// <summary>
/// Interface that every EventHandler class should implement it in order to be recognized as an event handler.
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public interface IEventHandler<in TEvent>
    where TEvent : IEvent
{
    void Handle(TEvent @event);
}