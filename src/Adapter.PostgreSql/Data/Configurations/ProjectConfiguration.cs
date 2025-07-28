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
                   value => ProjectId.NewEfId(value)
               )
               .HasColumnName(Project.ColumnNames.Id)
               .ValueGeneratedNever();


    }

    protected override void ConfigurateProperties(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(Project.Rules.NAME_MAX_LENGTH)
               .HasColumnName(Project.ColumnNames.Name);

        builder.ComplexProperty(x => x.ProjectPeriod, periodBuilder =>
        {
            periodBuilder.Property(x => x.StartDate)
                        .IsRequired()
                        .HasColumnName(Project.ColumnNames.StartDate)
                        .HasColumnType("datetime");

            periodBuilder.Property(x => x.EndDate)
                         .IsRequired(false)
                         .HasColumnName(Project.ColumnNames.EndDate)
                         .HasColumnType("datetime");
        });

        builder.ComplexProperty(x => x.Status, statusBuilder =>
        {
            statusBuilder.Property(x => x.Value)
                         .HasConversion<string>()
                         .IsRequired()
                         .HasMaxLength(Project.Rules.STATUS_MAX_LENGTH)
                         .HasColumnName(Project.ColumnNames.Status);
        });

        BaseEntityConfig.ApplyTo<Project, ProjectId>(builder);
    }
}