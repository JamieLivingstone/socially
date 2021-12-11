using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
  public DbSet<Comment> Comments { get; set; }

  public DbSet<Follower> Followers { get; set; }

  public DbSet<Like> Likes { get; set; }

  public DbSet<Person> Persons { get; set; }

  public DbSet<Post> Posts { get; set; }

  public DbSet<PostTag> PostTags { get; set; }

  public DbSet<Tag> Tags { get; set; }

  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
