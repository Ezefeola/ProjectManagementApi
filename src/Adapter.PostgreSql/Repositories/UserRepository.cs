using Adapter.SqlServer.Data;
using Core.Contracts.Repositories;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;

namespace Adapter.SqlServer.Repositories;
public class UserRepository : GenericRepository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}