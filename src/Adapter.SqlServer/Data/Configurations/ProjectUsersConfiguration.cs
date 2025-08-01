﻿using Core.Domain.Projects.Entities;
using Core.Domain.Projects.ValueObjects;
using Core.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.SqlServer.Data.Configurations;
public class ProjectUsersConfiguration : EntityTypeBaseConfiguration<ProjectUser>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<ProjectUser> builder)
    {
        //builder.HasKey(ProjectUser.ColumnNames.ProjectId, ProjectUser.ColumnNames.UserId);
        builder.HasKey(x => new {x.ProjectId, x.UserId });
        builder.Property(x => x.ProjectId)
               .HasConversion(
                    projectId => projectId.Value,
                    value => ProjectId.Create(value)
               )
               .HasColumnName(ProjectUser.ColumnNames.ProjectId)
               .ValueGeneratedNever();

        builder.Property(x => x.UserId)
               .HasConversion(
                    userId => userId.Value,
                    value => UserId.Create(value)
               )
               .HasColumnName(ProjectUser.ColumnNames.UserId)
               .ValueGeneratedNever();
        //builder.ComplexProperty(x => x.Id, idBuilder =>
        //{
        //    idBuilder.Property(x => x.ProjectId)
        //             .HasConversion(
        //                projectId => projectId.Value,
        //                value => ProjectId.Create(value)
        //             )
        //            .HasColumnName(ProjectUser.ColumnNames.ProjectId)
        //            .ValueGeneratedNever();

        //    idBuilder.Property(x => x.UserId)
        //             .HasConversion(
        //                userId => userId.Value,
        //                value => UserId.Create(value)
        //             )
        //            .HasColumnName(ProjectUser.ColumnNames.UserId)
        //            .ValueGeneratedNever();

        //});

        //builder.Property(x => x.Id.ProjectId)
        //      .HasConversion(
        //          projectId => projectId.Value,
        //          value => ProjectId.NewEfId(value)
        //      )
        //      .HasColumnName(ProjectUser.ColumnNames.ProjectId);
        builder.HasOne(x => x.Project)
               .WithMany(x => x.ProjectUsers)
               .HasForeignKey(x => x.ProjectId);

        //builder.Property(x => x.Id.CollaboratorId)
        //      .HasConversion(
        //          collaboratorId => collaboratorId.Value,
        //          value => CollaboratorId.NewEfId(value)
        //      )
        //      .HasColumnName(ProjectCollaborator.ColumnNames.CollaboratorId);
        builder.HasOne(x => x.User)
               .WithMany(x => x.ProjectUsers)
               .HasForeignKey(x => x.UserId);
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<ProjectUser> builder)
    {
        BaseEntityConfig.ApplyTo<ProjectUser>(builder);
    }
}