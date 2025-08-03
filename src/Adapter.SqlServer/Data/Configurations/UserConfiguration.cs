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
               .HasColumnName(nameof(User.Id))
               .ValueGeneratedNever();
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<User> builder)
    {
        builder.ComplexProperty(f => f.FullName, fullNameBuilder =>
        {
            fullNameBuilder.Property(x => x.FirstName)
                           .IsRequired()
                           .HasMaxLength(User.Rules.FIRST_NAME_MAX_LENGTH)
                           .HasColumnName(nameof(User.FullName.FirstName));

            fullNameBuilder.Property(x => x.LastName)
                           .IsRequired()
                           .HasMaxLength(User.Rules.LAST_NAME_MAX_LENGTH)
                           .HasColumnName(nameof(User.FullName.LastName));
        });

        builder.OwnsOne(x => x.EmailAddress, emailBuilder =>
        {
            emailBuilder.Property(x => x.Value)
                        .IsRequired()
                        .HasMaxLength(User.Rules.EMAIL_MAX_LENGTH)
                        .HasColumnName(nameof(User.EmailAddress));

            emailBuilder.HasIndex(x => x.Value)
                        .IsUnique();
        });

        builder.Property(x => x.Role)
               .IsRequired()
               .HasConversion(
                    x => x.Value,
                    value => UserRole.Create(value).Value
               )
               .HasColumnName(nameof(User.Role));

        builder.Property(x => x.Password)
               .HasColumnName(nameof(User.Password));    

        BaseEntityConfig.ApplyTo<User>(builder);
    }
}