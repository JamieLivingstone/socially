using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
  public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
  {
    public void Configure(EntityTypeBuilder<Follower> builder)
    {
      builder.HasKey(f => new { f.ObserverId, f.TargetId });

      builder.HasOne(f => f.Observer)
        .WithMany(o => o.Followers)
        .HasForeignKey(f => f.ObserverId)
        .OnDelete(DeleteBehavior.Cascade);

      builder.HasOne(f => f.Target)
        .WithMany(t => t.Following)
        .HasForeignKey(f => f.TargetId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
