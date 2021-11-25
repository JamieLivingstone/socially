using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Application.Posts.Commands.UpdatePost;
using FunctionalTests.Api.TestUtils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Posts
{
  [TestFixture]
  public class UpdatePost : TestBase
  {
    [Test]
    public async Task GivenAuthenticationFails_ReturnsUnauthorized()
    {
      var target = Seed.Posts().First();
      var content = new StringContent(JsonConvert.SerializeObject(new UpdatePostCommand { Slug = target.Slug }), Encoding.UTF8, "application/json-patch+json");

      var response = await AnonymousClient.PatchAsync($"/api/v1/posts/{target.Slug}", content);

      Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Test]
    public async Task GivenPostDoesNotExist_ReturnsNotFound()
    {
      var content = new StringContent(JsonConvert.SerializeObject(new UpdatePostCommand { Slug = "does-not-exist" }), Encoding.UTF8, "application/json-patch+json");

      var response = await AuthenticatedClient.PatchAsync($"/api/v1/posts/does-not-exist", content);

      Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task GivenPostAuthorIsNotCurrentUser_ReturnsForbidden()
    {
      var target = Seed.Posts().First(c => c.AuthorId != Seed.CurrentUserId);
      var content = new StringContent(JsonConvert.SerializeObject(new UpdatePostCommand { Slug = target.Slug }), Encoding.UTF8, "application/json-patch+json");

      var response = await AuthenticatedClient.PatchAsync($"/api/v1/posts/{target.Slug}", content);

      Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Test]
    public async Task GivenAValidRequest_ReturnsOk()
    {
      var target = Seed.Posts().First(c => c.AuthorId == Seed.CurrentUserId);
      var content = new StringContent(JsonConvert.SerializeObject(new UpdatePostCommand
      {
        Slug = target.Slug,
        Title = "Updated title"
      }), Encoding.UTF8, "application/json-patch+json");

      var response = await AuthenticatedClient.PatchAsync($"/api/v1/posts/{target.Slug}", content);

      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
  }
}
