using Framework.DomainModel.Entities;
using Sample.Core.DomainModel.UserAggregate.Events;
using Sample.Core.DomainModel.UserAggregate.ValueObjects;
using Sharpee.Framework.DomainModel.Entities;
using Sharpee.Framework.DomainModel.Events;
using Sharpee.Framework.DomainModel.Exceptions;

namespace Sample.Core.DomainModel.UserAggregate.Entities;

public sealed class User : AggregateRoot<Guid>, IAuditable<Guid>
{
    private User()
    { }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        Guid createdBy
    )
    {
        var user = new User();

        user.Emit(new UserCreated
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            CreatedBy = createdBy,
            CreatedOn = DateTime.UtcNow,
        });

        return user;
    }

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Email Email { get; private set; } = Email.Default;
    public Guid CreatedBy { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public Guid? ModifiedBy { get; private set; }
    public DateTime? ModifiedOn { get; private set; }

    public void Modify(
        string firstName,
        string lastName,
        string email,
        Guid modifiedBy
    )
    {
        Emit(new UserUpdated
        {
            Id = Id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            ModifiedBy = modifiedBy,
            ModifiedOn = DateTime.UtcNow,
        });
    }

    public void Remove()
    {
        Emit(new UserRemoved
        {
            Id = Id,
        });
    }

    protected override void SetStatesUsingEvents(IEvent @event)
    {
        switch (@event)
        {
            case UserCreated e:
                Id = e.Id;
                FirstName = e.FirstName;
                LastName = e.LastName;
                Email = e.Email;
                CreatedBy = e.CreatedBy;
                CreatedOn = e.CreatedOn;
                break;
            case UserUpdated e:
                FirstName = e.FirstName;
                LastName = e.LastName;
                Email = e.Email;
                ModifiedBy = e.ModifiedBy;
                ModifiedOn = e.ModifiedOn;
                break;
            case UserRemoved e:
                break;
            default:
                throw new SharpeeException("There is no such an event for email.");
        }
    }

    protected override void ValidateInvariants()
    {
        if (string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
        {
            throw new SharpeeException(
                "First name and last name could NOT be both null of whitspaces.",
                ExceptionStatus.InvalidArgument
            );
        }
    }
}
