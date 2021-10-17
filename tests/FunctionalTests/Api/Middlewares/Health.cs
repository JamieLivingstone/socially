using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.Middlewares
{
  [TestFixture]
  public class Health : TestBase
  {
    [Test]
    public async Task GivenRouteDoesNotExist_ReturnsNotFound()
    {
      var response = await AnonymousClient.GetAsync("does-not-exist");

      Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
  }
}
