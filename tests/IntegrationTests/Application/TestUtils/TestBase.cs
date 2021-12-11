using System;
using System.Collections.Generic;
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
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Web;

namespace IntegrationTests.Application.TestUtils;

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

        services.AddScoped<ICurrentUserService, MockCurrentUserService>();
      })
      .Build();

    _scopeFactory = host.Services.GetService<IServiceScopeFactory>();
  }

  [SetUp]
  public void SetUp()
  {
    using var scope = _scopeFactory.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    Seed.InitializeDbForTests(dbContext);
  }

  [TearDown]
  public void TearDown()
  {
    using var scope = _scopeFactory.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.EnsureDeleted();
  }

  protected static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
  {
    using var scope = _scopeFactory.CreateScope();

    var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

    return await mediator.Send(request);
  }

  protected static async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool eager = false) where TEntity : class
  {
    using var scope = _scopeFactory.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var query = context.Set<TEntity>().AsQueryable();

    if (eager)
    {
      var navigations = context.Model.FindEntityType(typeof(TEntity))
        ?.GetDerivedTypesInclusive()
        .SelectMany(type => type.GetNavigations())
        .Distinct() ?? new List<INavigation>();

      query = navigations.Aggregate(query, (current, property) => current.Include(property.Name));
    }

    return await query.FirstOrDefaultAsync(predicate);
  }
}
