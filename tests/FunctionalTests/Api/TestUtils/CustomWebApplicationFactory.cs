using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Accounts.Commands.Login;
using Application.Accounts.Commands.Register;
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
          var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

          dbContext.Database.EnsureDeleted();

          try
          {
            Seed.InitializeDbForTests(dbContext);
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

    public async Task<HttpClient> GetAuthenticatedClientAsync()
    {
      var client = CreateClient();

      await client.PostAsJsonAsync("/api/v1/accounts/register", new RegisterCommand
      {
        Name = "Test Account",
        Username = "test",
        Email = "test@test.com",
        Password = "Password@123",
      });

      var response = await client.PostAsJsonAsync("/api/v1/accounts/login", new LoginCommand
      {
        Username = "test",
        Password = "Password@123",
      });

      var loginResponse = await response.Content.ReadFromJsonAsync<LoginDto>();

      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse?.Token);

      return client;
    }
  }
}
