using Core.Domain.Users.Entities;
using Core.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.SqlServer.Data.Configurations;
public class RoleConfig : EntityTypeBaseConfiguration<Role>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasConversion(
                  id => id.Value,
                  value => RoleId.NewEfId(value)
               )
               .HasColumnName(Role.ColumnNames.Id)
               .ValueGeneratedOnAdd();
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name)
               .HasMaxLength(Role.Rules.NAME_MAX_LENGTH);

        BaseEntityConfig.ApplyTo<Role, RoleId>(builder);
    }
}