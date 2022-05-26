using Sharpee.Framework.DomainModel.Events;
using Sharpee.Framework.DomainModel.Exceptions;
using System;

namespace Sharpee.Framework.DomainModel.Entities;

/// <summary>
/// Abstract class to be inherited by entities.
/// </summary>
/// <typeparam name="TId">Type of aggregate root Id</typeparam>
public abstract class Entity<TId> where TId : IEquatable<TId>
{
    private readonly Action<IEvent> _eventLogic;

    /// <summary>
    /// Type of entity Id
    /// </summary>
    public TId Id { get; protected set; }

    /// <summary>
    /// Initiates an entity which gets its logic on event ocurring from outside.
    /// </summary>
    /// <param name="eventLogic">Logic to be happened when an event emits.</param>
    public Entity(Action<IEvent> eventLogic)
    {
        _eventLogic = eventLogic;
    }

    protected Entity() { }

    /// <summary>
    /// Identifies state if specified event occurs.
    /// </summary>
    /// <param name="event"></param>
    protected abstract void SetStateByEvent(IEvent @event);

    /// <summary>
    /// Method to handle event.
    /// </summary>
    /// <param name="event">event</param>
    public void Emit(IEvent @event)
    {
        SetStateByEvent(@event);

        if (@event == null)
        {
            throw new SharpeeException(logMessage: "Event can NOT be null.");
        }

        _eventLogic.Invoke(@event);
    }

    /// <summary>
    /// Checks equality of object with another object.
    /// </summary>
    /// <param name="obj">other object</param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (
            obj is not Entity<TId> other ||
            GetType() != other.GetType() ||
            Id is null ||
            other.Id is null
        )
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TId> a, Entity<TId> b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TId> a, Entity<TId> b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Returns object hash code.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}