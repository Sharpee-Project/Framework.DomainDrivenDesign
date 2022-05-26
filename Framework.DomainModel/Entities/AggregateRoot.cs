using Sharpee.Framework.DomainModel.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpee.Framework.DomainModel.Entities;

/// <summary>
/// Abstract class to be inherited by aggregate roots.
/// </summary>
/// <typeparam name="TId">Type of aggregate root Id</typeparam>
public abstract class AggregateRoot<TId> where TId : IEquatable<TId>
{
    private readonly ICollection<IEvent> _events;

    /// <summary>
    /// Type of aggregate root Id
    /// </summary>
    public TId Id { get; protected set; }

    protected AggregateRoot()
    {
        _events = new HashSet<IEvent>();
    }

    /// <summary>
    /// Identifies state if specified event occurs.
    /// </summary>
    /// <param name="event"></param>
    protected abstract void SetStatesUsingEvents(IEvent @event);

    /// <summary>
    /// Validates invariants of the object.
    /// </summary>
    protected abstract void ValidateInvariants();

    /// <summary>
    /// Method to handle event.
    /// </summary>
    /// <param name="event">event</param>
    protected void Emit(IEvent @event)
    {
        SetStatesUsingEvents(@event);
        ValidateInvariants();
        _events.Add(@event);
    }

    /// <summary>
    /// Returns all object events.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IEvent> GetEvents()
    {
        return _events.AsEnumerable();
    }

    /// <summary>
    /// Clears all object events.
    /// </summary>
    public void ClearEvents()
    {
        _events.Clear();
    }

    /// <summary>
    /// Checks equality of object with another object.
    /// </summary>
    /// <param name="obj">other object</param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj is not AggregateRoot<TId> other ||
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

    public static bool operator ==(AggregateRoot<TId> left, AggregateRoot<TId> right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(AggregateRoot<TId> left, AggregateRoot<TId> right)
    {
        return !(left == right);
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