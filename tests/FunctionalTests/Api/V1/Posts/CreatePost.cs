using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Posts.Commands.CreatePost;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Posts
{
  [TestFixture]
  public class CreatePost : TestBase
  {
    [Test]
    public async Task GivenAuthenticationFails_ReturnsUnauthorized()
    {
      var response = await AnonymousClient.PostAsJsonAsync("/api/v1/posts", new CreatePostCommand
      {
        Title = "Test title",
        Body = "Test comment"
      });

      Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Test]
    public async Task GivenValidationFails_ReturnsBadRequest()
    {
      var response = await AuthenticatedClient.PostAsJsonAsync("/api/v1/posts", new CreatePostCommand());

      Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task GivenValidRequest_ReturnsCreated()
    {
      var response = await AuthenticatedClient.PostAsJsonAsync("/api/v1/posts", new CreatePostCommand
      {
        Title = "Test title",
        Body = "Test comment"
      });

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
  }
}
