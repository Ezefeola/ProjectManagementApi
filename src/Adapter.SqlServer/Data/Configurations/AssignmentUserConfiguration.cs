using Core.Domain.Projects.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapter.SqlServer.Data.Configurations;
public class AssignmentUserConfiguration : EntityTypeBaseConfiguration<AssignmentUser>
{
    protected override void ConfigurateConstraints(EntityTypeBuilder<AssignmentUser> builder)
    {
        builder.HasKey(x => new { x.AssignmentId, x.UserId });

        builder.HasOne(x => x.Assignment)
               .WithMany(x => x.AssignmentUsers)
               .HasForeignKey(x => x.AssignmentId);

        builder.HasOne(x => x.User)
               .WithMany(x => x.AssignmentUsers)
               .HasForeignKey(x => x.UserId);
    }

    protected override void ConfigurateProperties(EntityTypeBuilder<AssignmentUser> builder)
    {
        BaseEntityConfig.ApplyTo<AssignmentUser>(builder);
    }
}