using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Comments.Commands.CreateComment;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Comments
{
  [TestFixture]
  public class CreateComment : TestBase
  {
    [Test]
    public async Task GivenAuthenticationFails_ReturnsUnauthorized()
    {
      var target = Seed.Posts().First();

      var response = await AnonymousClient.PostAsJsonAsync($"/api/v1/posts/{target.Slug}/comments", new CreateCommentCommand
      {
        Message = "message",
        Slug = target.Slug,
      });

      Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Test]
    public async Task GivenValidationFails_ReturnsBadRequest()
    {
      var target = Seed.Posts().First();

      var response = await AuthenticatedClient.PostAsJsonAsync($"/api/v1/posts/{target.Slug}/comments", new CreateCommentCommand
      {
        Message = "", // Required field
        Slug = target.Slug,
      });

      Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task GivenAValidRequest_ReturnsCreated()
    {
      var target = Seed.Posts().First();

      var response = await AuthenticatedClient.PostAsJsonAsync($"/api/v1/posts/{target.Slug}/comments", new CreateCommentCommand
      {
        Message = "Message",
        Slug = target.Slug,
      });

      Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
  }
}
