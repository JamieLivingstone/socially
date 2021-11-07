using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Web;

namespace IntegrationTests.Application.TestUtils
{
  public class TestBase
  {
    private static IServiceScopeFactory _scopeFactory;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      var host = WebHost.CreateDefaultBuilder()
        .UseStartup<Startup>()
        .ConfigureTestServices(services =>
        {
          var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

          services.Remove(descriptor);

          services.AddDbContext<ApplicationDbContext>(options =>
          {
            options.UseInMemoryDatabase("IntegrationTests");
          });

          services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

          var sp = services.BuildServiceProvider();

          using var scope = sp.CreateScope();
          var scopedServices = scope.ServiceProvider;
          var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();

          dbContext.Database.EnsureDeleted();
        })
        .Build();

      _scopeFactory = host.Services.GetService<IServiceScopeFactory>();
    }

    protected static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
      using var scope = _scopeFactory.CreateScope();

      var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

      return await mediator.Send(request);
    }

    protected static async Task<TEntity> FindByIdAsync<TEntity>(params object[] key) where TEntity : class
    {
      using var scope = _scopeFactory.CreateScope();

      var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

      return await context.FindAsync<TEntity>(key);
    }

    protected static async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
      using var scope = _scopeFactory.CreateScope();

      var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

      return await context.Set<TEntity>().FirstAsync(predicate);
    }
  }
}