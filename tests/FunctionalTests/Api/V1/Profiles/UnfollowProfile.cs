using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Profiles;

[TestFixture]
public class UnfollowProfile : TestBase
{
  [Test]
  public async Task GivenAuthenticationFails_ReturnsUnauthorized()
  {
    var username = Seed.Persons().First(p => p.Id != Seed.CurrentUserId).Username;

    var response = await AnonymousClient.DeleteAsync($"/api/v1/profiles/{username}/followers");

    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Test]
  public async Task GivenProfileDoesNotExist_ReturnsNotFound()
  {
    const string username = "does_not_exist";

    var response = await AuthenticatedClient.DeleteAsync($"/api/v1/profiles/{username}/followers");

    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [Test]
  public async Task GivenAValidRequest_ReturnsOk()
  {
    var username = Seed.Persons().First(p => p.Id != Seed.CurrentUserId).Username;

    var response = await AuthenticatedClient.DeleteAsync($"/api/v1/profiles/{username}/followers");

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
  }
}
