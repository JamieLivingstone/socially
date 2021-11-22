using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Accounts.Commands.Login;
using Application.Accounts.Commands.Register;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web;

namespace FunctionalTests.Api.TestUtils
{
  public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
  {
    private string _bearerToken;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      builder
        .ConfigureServices(services =>
        {
          var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

          services.Remove(descriptor);

          services.AddDbContext<ApplicationDbContext>(options =>
          {
            options.UseInMemoryDatabase("FunctionalTests");
          });

          var sp = services.BuildServiceProvider();

          using var scope = sp.CreateScope();
          var scopedServices = scope.ServiceProvider;
          var dbContext = scopedServices.GetRequiredService<ApplicationDbContext>();
          var jwtTokenGenerator = scopedServices.GetRequiredService<IJwtTokenGenerator>();
          var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

          dbContext.Database.EnsureDeleted();

          try
          {
            Seed.InitializeDbForTests(dbContext);
            _bearerToken = jwtTokenGenerator.CreateToken(Seed.CurrentUserId);
          }
          catch (Exception ex)
          {
            logger.LogError(ex, "An error occurred seeding the " + "database with test messages. Error: {Message}", ex.Message);
          }
        });
    }

    public HttpClient GetAnonymousClient()
    {
      return CreateClient();
    }

    public HttpClient GetAuthenticatedClientAsync()
    {
      var client = CreateClient();

      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);

      return client;
    }
  }
}
