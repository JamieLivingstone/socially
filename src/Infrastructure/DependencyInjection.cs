using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Security;
using Infrastructure.Security.Options;
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
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
          b => { b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName); });
      });

      services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

      services.Configure<PasswordHasherOptions>(configuration.GetSection("Security:PasswordHasherOptions"));

      services.AddTransient<IPasswordHasher, PasswordHasher>();

      services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
    }
  }
}
