using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
{
  public void Configure(EntityTypeBuilder<PostTag> builder)
  {
    builder.HasKey(pt => new { pt.PostId, pt.TagId });

    builder.HasOne(pt => pt.Post)
      .WithMany(p => p.PostTags)
      .HasForeignKey(pt => pt.PostId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(pt => pt.Tag)
      .WithMany(t => t.PostTags)
      .HasForeignKey(pt => pt.TagId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
