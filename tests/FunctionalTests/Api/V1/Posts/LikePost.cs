using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Posts
{
  [TestFixture]
  public class LikePost : TestBase
  {
    [Test]
    public async Task GivenAuthenticationFails_ReturnsUnauthorized()
    {
      var slug = Seed.Posts().First().Slug;

      var response = await AnonymousClient.PostAsync($"/api/v1/posts/{slug}/likes", null);

      Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Test]
    public async Task GivenPostDoesNotExist_ReturnsNotFound()
    {
      const string slug = "does-not-exist";

      var response = await AuthenticatedClient.PostAsync($"/api/v1/posts/{slug}/likes", null);

      Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task GivenAValidRequest_ReturnsCreated()
    {
      var slug = Seed.Posts().First().Slug;

      var response = await AuthenticatedClient.PostAsync($"/api/v1/posts/{slug}/likes", null);

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
  }
}
