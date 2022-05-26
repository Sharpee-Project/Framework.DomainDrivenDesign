using Sharpee.Framework.DomainModel.Events;

namespace Sample.Core.DomainModel.UserAggregate.Events;

public sealed class UserRemoved : IEvent
{
    public Guid EventId { get; set; }
    public Guid Id { get; init; }
}