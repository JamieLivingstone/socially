using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Comments
{
  [TestFixture]
  public class DeleteComment : TestBase
  {
    [Test]
    public async Task GivenAuthenticationFails_ReturnsUnauthorized()
    {
      var targetComment = Seed.Comments().First(c => c.AuthorId == Seed.CurrentUserId);
      var targetPost = Seed.Posts().First(p => p.Id == targetComment.PostId);

      var response = await AnonymousClient.DeleteAsync($"/api/v1/posts/{targetPost.Slug}/comments/{targetComment.Id}");

      Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Test]
    public async Task GivenCommentDoesNotExist_ReturnsNotFound()
    {
      var targetPost = Seed.Posts().First();

      var response = await AuthenticatedClient.DeleteAsync($"/api/v1/posts/{targetPost.Slug}/comments/{int.MinValue}");

      Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Test]
    public async Task GivenPostAuthorIsNotCurrentUser_ReturnsForbidden()
    {
      var targetComment = Seed.Comments().First(c => c.AuthorId != Seed.CurrentUserId);

      var response = await AuthenticatedClient.DeleteAsync($"/api/v1/posts/does-not-exist/comments/{targetComment.Id}");

      Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Test]
    public async Task GivenAValidRequest_ReturnsOk()
    {
      var targetComment = Seed.Comments().First(c => c.AuthorId == Seed.CurrentUserId);
      var targetPost = Seed.Posts().First(p => p.Id == targetComment.PostId);

      var response = await AuthenticatedClient.DeleteAsync($"/api/v1/posts/{targetPost.Slug}/comments/{targetComment.Id}");

      Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
  }
}
