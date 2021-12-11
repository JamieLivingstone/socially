using System.Reflection;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
  public DbSet<Comment> Comments { get; set; }

  public DbSet<Follower> Followers { get; set; }

  public DbSet<Like> Likes { get; set; }

  public DbSet<Person> Persons { get; set; }

  public DbSet<Post> Posts { get; set; }

  public DbSet<PostTag> PostTags { get; set; }

  public DbSet<Tag> Tags { get; set; }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}
