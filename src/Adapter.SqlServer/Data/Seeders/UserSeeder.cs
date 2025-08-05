using Core.Domain.Common.DomainResults;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Adapter.SqlServer.Data.Seeders;
public static class UserSeeder
{
    public async static Task Seed(DbContext dbContext)
    {
        if(!await dbContext.Set<User>().AnyAsync())
        {
            DomainResult<User> userResult = User.Create(
                "Ezequiel",
                "Feola",
                "ezefeola@gmail.com",
                "test123",
                UserRole.UserRolesEnum.Admin
            );

            if (userResult.IsSuccess)
            {
                await dbContext.Set<User>().AddAsync(userResult.Value);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}