using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
  public interface IApplicationDbContext
  {
    public DbSet<Person> Persons { get; set; }

    public DbSet<Post> Posts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
  }
}
