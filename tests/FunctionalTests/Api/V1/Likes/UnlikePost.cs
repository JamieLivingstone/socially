using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Likes;

[TestFixture]
public class UnlikePost : TestBase
{
  [Test]
  public async Task GivenAuthenticationFails_ReturnsUnauthorized()
  {
    var slug = Seed.Posts().First().Slug;

    var response = await AnonymousClient.DeleteAsync($"/api/v1/posts/{slug}/likes");

    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Test]
  public async Task GivenPostDoesNotExist_ReturnsNotFound()
  {
    const string slug = "does-not-exist";

    var response = await AuthenticatedClient.DeleteAsync($"/api/v1/posts/{slug}/likes");

    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [Test]
  public async Task GivenAValidRequest_ReturnsOk()
  {
    var slug = Seed.Posts().First().Slug;

    var response = await AuthenticatedClient.DeleteAsync($"/api/v1/posts/{slug}/likes");

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
  }
}
