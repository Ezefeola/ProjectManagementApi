using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IUserRepository : IGenericRepository<User, UserId>
{
}
