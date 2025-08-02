using Core.Domain.Projects;
using Core.Domain.Projects.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.SqlServer.Data.Configurations;
public class ProjectConfiguration : EntityTypeBaseConfiguration<Project>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasConversion(
                   id => id.Value,
                   value => ProjectId.Create(value)
               )
               .HasColumnName(nameof(Project.Id))
               .ValueGeneratedNever();
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(Project.Rules.NAME_MAX_LENGTH)
               .HasColumnName(nameof(Project.Name));

        builder.ComplexProperty(x => x.ProjectPeriod, periodBuilder =>
        {
            periodBuilder.Property(x => x.StartDate)
                        .IsRequired()
                        .HasColumnName(nameof(Project.ProjectPeriod.StartDate))
                        .HasColumnType("datetime");

            periodBuilder.Property(x => x.EndDate)
                         .IsRequired()
                         .HasColumnName(nameof(Project.ProjectPeriod.EndDate))
                         .HasColumnType("datetime");
        });

        builder.ComplexProperty(x => x.Status, statusBuilder =>
        {
            statusBuilder.Property(x => x.Value)
                         .HasConversion<string>()
                         .IsRequired()
                         .HasMaxLength(Project.Rules.STATUS_MAX_LENGTH)
                         .HasColumnName(nameof(Project.Status));
        });

        BaseEntityConfig.ApplyTo<Project>(builder);
    }
}