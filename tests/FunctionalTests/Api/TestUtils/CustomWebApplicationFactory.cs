using System.Linq;
using System.Net.Http;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
          var dbContextDescriptor =
            services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

          if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

          services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("FunctionalTests"); });
        });
    }

    public HttpClient GetAnonymousClient()
    {
      return CreateClient();
    }
  }
}
