using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.SqlServer.Data.Configurations;
public class AssignmentConfiguration : EntityTypeBaseConfiguration<Assignment>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasConversion(
                   id => id.Value,
                   value => AssignmentId.Create(value)
               )
               .HasColumnName(Assignment.ColumnNames.Id)
               .ValueGeneratedNever();

        builder.HasOne(x => x.Project)
               .WithMany(x => x.Assignments)
               .HasForeignKey(x => x.ProjectId);

        builder.HasOne(x => x.User)
               .WithMany(x => x.Assignments)
               .HasForeignKey(x => x.UserId);
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<Assignment> builder)
    {
        builder.Property(x => x.Title)
               .HasMaxLength(Assignment.Rules.TITLE_MAX_LENGTH)
               .HasColumnName(Assignment.ColumnNames.Title);

        builder.Property(x => x.Description)
               .HasMaxLength(Assignment.Rules.DESCRIPTION_MAX_LENGTH)
               .HasColumnName(Assignment.ColumnNames.Description);

        builder.Property(x => x.EstimatedHours)
               .HasPrecision(18, 2)
               .HasColumnName(Assignment.ColumnNames.EstimatedHours);

        builder.Property(x => x.LoggedHours)
               .HasPrecision(18, 2)
               .HasColumnName(Assignment.ColumnNames.LoggedHours);

        builder.ComplexProperty(x => x.Status, assignmentStatusBuilder =>
        {
            assignmentStatusBuilder.Property(x => x.Value)
                .HasConversion<string>()
                .HasColumnName(Assignment.ColumnNames.Status);
        });

        BaseEntityConfig.ApplyTo<Assignment>(builder);
    }
}