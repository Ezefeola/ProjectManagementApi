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
               .HasColumnName(nameof(Assignment.Id))
               .ValueGeneratedNever();

        builder.HasOne(x => x.Project)
               .WithMany(x => x.Assignments)
               .HasForeignKey(x => x.ProjectId);
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<Assignment> builder)
    {
        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(Assignment.Rules.TITLE_MAX_LENGTH)
               .HasColumnName(nameof(Assignment.Title));

        builder.Property(x => x.Description)
               .IsRequired(false)
               .HasMaxLength(Assignment.Rules.DESCRIPTION_MAX_LENGTH)
               .HasColumnName(nameof(Assignment.Description));

        builder.Property(x => x.EstimatedHours)
               .IsRequired(false)
               .HasPrecision(18, 2)
               .HasColumnName(nameof(Assignment.EstimatedHours));

        builder.Property(x => x.LoggedHours)
               .IsRequired(false)
               .HasPrecision(18, 2)
               .HasColumnName(nameof(Assignment.LoggedHours));

        builder.ComplexProperty(x => x.Status, assignmentStatusBuilder =>
        {
            assignmentStatusBuilder.Property(x => x.Value)
                                   .HasConversion<string>()
                                   .HasColumnName(nameof(Assignment.Status));
        });

        BaseEntityConfig.ApplyTo<Assignment>(builder);
    }
}