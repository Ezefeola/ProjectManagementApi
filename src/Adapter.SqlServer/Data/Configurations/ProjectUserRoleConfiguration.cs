using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.SqlServer.Data.Configurations;
public class ProjectUserRoleConfiguration : EntityTypeBaseConfiguration<ProjectUserRole>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<ProjectUserRole> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasConversion(
                    id => id.Value,
                    value => ProjectUserRoleId.Create(value)
               )
               .HasColumnName(nameof(ProjectUserRole.Id))
               .ValueGeneratedNever();
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<ProjectUserRole> builder)
    {
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(ProjectUserRole.Rules.NAME_MAX_LENGTH)
               .HasColumnName(nameof(ProjectUserRole.Name));
        builder.HasIndex(x => x.Name)
               .IsUnique();

        BaseEntityConfig.ApplyTo<ProjectUserRole>(builder);
    }
}