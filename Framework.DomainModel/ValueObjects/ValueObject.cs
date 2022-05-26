using System;

namespace Sharpee.Framework.DomainModel.ValueObjects;

/// <summary>
/// Abstract class to be inherited by value objects.
/// </summary>
/// <typeparam name="TValueObject">Type of value object</typeparam>
public abstract class ValueObject<TValueObject> : IEquatable<TValueObject>
        where TValueObject : ValueObject<TValueObject>
{
    /// <summary>
    /// Checks equality of object with another object.
    /// </summary>
    /// <param name="obj">other object</param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj is not TValueObject otherObject)
        {
            return false;
        }

        return DoesEqualTo(otherObject);
    }

    /// <summary>
    /// Checks equality of object with another object.
    /// </summary>
    /// <param name="other">other object</param>
    /// <returns></returns>
    public bool Equals(TValueObject other)
    {
        return this == other;
    }

    /// <summary>
    /// Checks equality of object with another object.
    /// </summary>
    /// <param name="otherObject"></param>
    /// <returns></returns>
    public abstract bool DoesEqualTo(TValueObject otherObject);

    /// <summary>
    /// Returns object hash code.
    /// </summary>
    /// <returns></returns>
    public override sealed int GetHashCode()
    {
        return SetHashCode();
    }

    /// <summary>
    /// Returns value object hash code.
    /// </summary>
    /// <returns></returns>
    public abstract int SetHashCode();

    public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
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

    public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
    {
        return !(left == right);
    }
}