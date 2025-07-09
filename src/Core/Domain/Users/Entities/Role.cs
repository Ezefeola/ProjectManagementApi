using Core.Domain.Abstractions;
using Core.Domain.Users.ValueObjects;

namespace Core.Domain.Users.Entities;
public class Role : Entity<RoleId>
{
    public static class ColumnNames
    {
        public const string Id = "Id";
    }
    public static class Rules
    {
        public const int NAME_MAX_LENGTH = 100;
    }
    public enum RolesEnum
    {
        Example
    }

    public string Name { get; set; } = default!;

    public IEnumerable<User> Users { get; set; } = default!;
}