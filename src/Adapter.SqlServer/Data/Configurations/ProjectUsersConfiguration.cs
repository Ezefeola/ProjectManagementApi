using Core.Domain.Projects.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.SqlServer.Data.Configurations;
public class ProjectUsersConfiguration : EntityTypeBaseConfiguration<ProjectUser>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<ProjectUser> builder)
    {
        builder.HasKey(x => new {x.ProjectId, x.UserId });
        
        builder.HasOne(x => x.Project)
               .WithMany(x => x.ProjectUsers)
               .HasForeignKey(x => x.ProjectId);

        builder.HasOne(x => x.User)
               .WithMany(x => x.ProjectUsers)
               .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.ProjectUserRole)
              .WithMany()
              .HasForeignKey(x => x.ProjectUserRoleId)
              .IsRequired();
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<ProjectUser> builder)
    {
        BaseEntityConfig.ApplyTo<ProjectUser>(builder);
    }
}