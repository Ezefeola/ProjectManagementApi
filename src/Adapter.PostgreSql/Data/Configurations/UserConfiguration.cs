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
                  value => UserId.NewEfId(value)
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
                           .HasMaxLength(User.Rules.FIRSTNAME_MAX_LENGTH)
                           .HasColumnName(User.ColumnNames.FirstName);

            fullNameBuilder.Property(x => x.LastName)
                           .IsRequired()
                           .HasMaxLength(User.Rules.LASTNAME_MAX_LENGTH)
                           .HasColumnName(User.ColumnNames.LastName);
        });

        builder.ComplexProperty(x => x.EmailAddress, emailBuilder =>
        {
            emailBuilder.Property(x => x.Value)
                        .IsRequired()
                        .HasMaxLength(User.Rules.EMAIL_MAX_LENGTH)
                        .HasColumnName(User.ColumnNames.Email);
        });
        builder.Property<string>(User.ColumnNames.Email)
               .HasColumnName(User.ColumnNames.Email);
        builder.HasIndex(User.ColumnNames.Email)
               .IsUnique();

        builder.Property(x => x.Password)
               .HasColumnName(User.ColumnNames.Password);    

        BaseEntityConfig.ApplyTo<User, UserId>(builder);
    }
}