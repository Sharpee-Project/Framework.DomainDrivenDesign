using Sharpee.Framework.DomainModel.Events;

namespace Sample.Core.DomainModel.UserAggregate.Events;

public sealed class UserUpdated : IEvent
{
    public Guid EventId { get; set; }
    public Guid Id { get; set; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public Guid ModifiedBy { get; init; }
    public DateTime ModifiedOn { get; init; }
}
