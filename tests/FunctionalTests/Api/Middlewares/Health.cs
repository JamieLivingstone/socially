using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.Middlewares;

[TestFixture]
public class Health
{
  private HttpClient _client;

  [SetUp]
  public void SetUp()
  {
    var factory = new CustomWebApplicationFactory();
    _client = factory.GetAnonymousClient();
  }

  [Test]
  public async Task GivenRouteDoesNotExist_ReturnsNotFound()
  {
    var response = await _client.GetAsync("does-not-exist");

    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }
}
