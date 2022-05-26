using Sharpee.Framework.DomainModel.Events;

namespace Sample.Core.DomainModel.UserAggregate.Events;

public sealed class UserCreated : IEvent
{
    public Guid EventId { get; set; }
    public Guid Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public Guid CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
}
