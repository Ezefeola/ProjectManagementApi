using Core.Domain.Collaborators.ValueObjects;
using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.SqlServer.Data.Configurations;
public class ProjectCollaboratorConfiguration : EntityTypeBaseConfiguration<ProjectUser>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<ProjectUser> builder)
    {
        builder.HasKey(ProjectUser.ColumnNames.ProjectId, ProjectUser.ColumnNames.CollaboratorId);
        builder.ComplexProperty(x => x.Id, idBuilder =>
        {
            idBuilder.Property(x => x.ProjectId)
                     .HasConversion(
                        projectId => projectId.Value,
                        value => ProjectId.NewEfId(value)
                     )
                    .HasColumnName(ProjectUser.ColumnNames.ProjectId)
                    .ValueGeneratedNever();

            idBuilder.Property(x => x.CollaboratorId)
                     .HasConversion(
                        collaboratorId => collaboratorId.Value,
                        value => CollaboratorId.NewEfId(value)
                     )
                    .HasColumnName(ProjectUser.ColumnNames.CollaboratorId)
                    .ValueGeneratedNever();
        });

        //builder.Property(x => x.Id.ProjectId)
        //      .HasConversion(
        //          projectId => projectId.Value,
        //          value => ProjectId.NewEfId(value)
        //      )
        //      .HasColumnName(ProjectCollaborator.ColumnNames.ProjectId);
        builder.HasOne(x => x.Project)
               .WithMany(x => x.ProjectCollaborators)
               .HasForeignKey(x => x.Id.ProjectId);

        //builder.Property(x => x.Id.CollaboratorId)
        //      .HasConversion(
        //          collaboratorId => collaboratorId.Value,
        //          value => CollaboratorId.NewEfId(value)
        //      )
        //      .HasColumnName(ProjectCollaborator.ColumnNames.CollaboratorId);
        builder.HasOne(x => x.Collaborator)
               .WithMany(x => x.ProjectCollaborators)
               .HasForeignKey(x => x.Id.CollaboratorId);
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<ProjectUser> builder)
    {
        BaseEntityConfig.ApplyTo<ProjectUser, ProjectUserId>(builder);
    }
}