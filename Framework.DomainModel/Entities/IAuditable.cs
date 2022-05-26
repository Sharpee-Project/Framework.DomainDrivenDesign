using System;

namespace Framework.DomainModel.Entities;

/// <summary>
/// Adds auditing properties to inherited class
/// </summary>
/// <typeparam name="T">Type of auditer's id</typeparam>
public interface IAuditable<T> where T : struct
{
    /// <summary>
    /// Creator's id
    /// </summary>
    public T CreatedBy { get; }

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreatedOn { get; }

    /// <summary>
    /// Modifier's id
    /// </summary>
    public T? ModifiedBy { get; }

    /// <summary>
    /// Modification date
    /// </summary>
    public DateTime? ModifiedOn { get; }
}