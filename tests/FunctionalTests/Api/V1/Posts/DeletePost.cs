using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Posts;

[TestFixture]
public class DeletePost : TestBase
{
  [Test]
  public async Task GivenAuthenticationFails_ReturnsUnauthorized()
  {
    var target = Seed.Posts().First();

    var response = await AnonymousClient.DeleteAsync($"/api/v1/posts/{target.Id}");

    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
  }

  [Test]
  public async Task GivenPostAuthorIsNotCurrentUser_ReturnsForbidden()
  {
    var target = Seed.Posts().First(p => p.AuthorId != Seed.CurrentUserId);

    var response = await AuthenticatedClient.DeleteAsync($"/api/v1/posts/{target.Slug}");

    Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
  }

  [Test]
  public async Task GivenAValidRequest_ReturnsOk()
  {
    var target = Seed.Posts().First(p => p.AuthorId == Seed.CurrentUserId);

    var response = await AuthenticatedClient.DeleteAsync($"/api/v1/posts/{target.Slug}");

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
  }
}
