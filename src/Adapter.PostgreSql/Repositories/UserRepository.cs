using Adapter.SqlServer.Data;
using Core.Contracts.Repositories;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SqlServer.Repositories;
public class UserRepository : GenericRepository<User, UserId>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .FirstOrDefaultAsync(x => x.EmailAddress.Value == email, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Query()
                     .AsNoTracking()
                     .AnyAsync(x => x.EmailAddress.Value == email, cancellationToken);
    }
}