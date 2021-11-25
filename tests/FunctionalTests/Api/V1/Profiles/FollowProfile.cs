using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Profiles
{
  [TestFixture]
  public class FollowProfile : TestBase
  {
    [Test]
    public async Task GivenAuthenticationFails_ReturnsUnauthorized()
    {
      var username = Seed.Persons().Last().Username;

      var response = await AnonymousClient.PostAsync($"/api/v1/profiles/{username}/followers", null);

      Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Test]
    public async Task GivenProfileDoesNotExist_ReturnsNotFound()
    {
      var username = "does_not_exist";

      var response = await AuthenticatedClient.PostAsync($"/api/v1/profiles/{username}/followers", null);

      Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task GivenTargetProfileIsCurrentUser_ReturnsUnprocessableEntity()
    {
      var username = Seed.Persons().First(p => p.Id == Seed.CurrentUserId).Username;

      var response = await AuthenticatedClient.PostAsync($"/api/v1/profiles/{username}/followers", null);

      Assert.AreEqual(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }

    [Test]
    public async Task GivenAValidRequest_ReturnsCreated()
    {
      var username = Seed.Persons().First(p => p.Id != Seed.CurrentUserId).Username;

      var response = await AuthenticatedClient.PostAsync($"/api/v1/profiles/{username}/followers", null);

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
  }
}
