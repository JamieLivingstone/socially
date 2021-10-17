using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
  public static class DependencyInjection
  {
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
      {
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => { b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName); });
      });
    }
  }
}
