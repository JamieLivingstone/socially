using System.Net.Http;
using NUnit.Framework;

namespace FunctionalTests.Api.TestUtils
{
  public class TestBase
  {
    protected HttpClient AnonymousClient;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      var factory = new CustomWebApplicationFactory();
      AnonymousClient = factory.GetAnonymousClient();
    }
  }
}
