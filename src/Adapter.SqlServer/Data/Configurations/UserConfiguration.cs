using Core.Domain.Abstractions.ValueObjects;
using Core.Domain.Users;
using Core.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.SqlServer.Data.Configurations;
public class UserConfiguration : EntityTypeBaseConfiguration<User>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasConversion(
                  id => id.Value,
                  value => UserId.Create(value)
               )
               .HasColumnName(User.ColumnNames.Id)
               .ValueGeneratedNever();
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<User> builder)
    {
        builder.ComplexProperty(f => f.FullName, fullNameBuilder =>
        {
            fullNameBuilder.Property(x => x.FirstName)
                           .IsRequired()
                           .HasMaxLength(User.Rules.FIRST_NAME_MAX_LENGTH)
                           .HasColumnName(User.ColumnNames.FirstName);

            fullNameBuilder.Property(x => x.LastName)
                           .IsRequired()
                           .HasMaxLength(User.Rules.LAST_NAME_MAX_LENGTH)
                           .HasColumnName(User.ColumnNames.LastName);
        });

        builder.Property(x => x.EmailAddress)
               .IsRequired()
               .HasConversion(
                    x => x.Value,
                    value => EmailAddress.Create(value).Value
               )
               .HasColumnName(User.ColumnNames.Email);
        builder.HasIndex(x => x.EmailAddress)
                   .IsUnique();

        builder.Property(x => x.UserRole)
               .IsRequired()
               .HasConversion(
                    x => x.Value,
                    value => UserRole.Create(value).Value
               )
               .HasColumnName(User.ColumnNames.UserRole);

        builder.Property(x => x.Password)
               .HasColumnName(User.ColumnNames.Password);    

        BaseEntityConfig.ApplyTo<User>(builder);
    }
}