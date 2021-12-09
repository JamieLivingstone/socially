using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
  public void Configure(EntityTypeBuilder<Like> builder)
  {
    builder.HasKey(l => new
    {
      l.ObserverId,
      l.PostId
    });

    builder.HasOne(l => l.Post)
      .WithMany(p => p.Likes)
      .HasForeignKey(l => l.PostId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(l => l.Observer)
      .WithMany(p => p.Likes)
      .HasForeignKey(l => l.ObserverId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
