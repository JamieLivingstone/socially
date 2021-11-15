using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
  public class PostConfiguration : IEntityTypeConfiguration<Post>
  {
    public void Configure(EntityTypeBuilder<Post> builder)
    {
      builder.Property(p => p.Slug)
        .IsRequired();

      builder.Property(p => p.Title)
        .IsRequired();

      builder.Property(p => p.Body)
        .IsRequired();

      builder.Property(p => p.CreatedAt)
        .IsRequired();

      builder.Property(p => p.UpdatedAt)
        .IsRequired();

      builder.HasIndex(p => p.Slug)
        .IsUnique();

      builder.HasOne(p => p.Author)
        .WithMany(a => a.Posts)
        .HasForeignKey(p => p.AuthorId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
