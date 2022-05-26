using Sample.Core.DomainModel.UserAggregate.Entities;

namespace Sample.Core.DomainModel.UserAggregate.Data;

public interface IUserRepository
{
    User Create(User user);
    User GetById(Guid id);
    IEnumerable<User> Search(string? searchQuery = null);
    User Remove(Guid id);
    bool Exists(Guid userId);
}
