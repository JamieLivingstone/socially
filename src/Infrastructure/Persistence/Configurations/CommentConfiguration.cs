using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
  public class CommentConfiguration : IEntityTypeConfiguration<Comment>
  {
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
      builder.HasOne(c => c.Post)
        .WithMany(p => p.Comments)
        .HasForeignKey(c => c.PostId)
        .OnDelete(DeleteBehavior.Cascade);

      builder.HasOne(c => c.Author)
        .WithMany(a => a.Comments)
        .HasForeignKey(c => c.AuthorId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
