using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;

namespace Core.Contracts.Repositories;
public interface IUserRepository : IGenericRepository<User, UserId>
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
