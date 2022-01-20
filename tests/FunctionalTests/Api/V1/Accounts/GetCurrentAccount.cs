using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Accounts;

[TestFixture]
public class GetCurrentAccount : TestBase
{
  [Test]
  public async Task GivenAuthenticationFails_ReturnsUnauthorized()
  {
    var response = await AnonymousClient.GetAsync("/api/v1/accounts/current");

    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Test]
  public async Task GivenAValidRequest_ReturnsOk()
  {
    var response = await AuthenticatedClient.GetAsync("/api/v1/accounts/current");

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
  }
}
