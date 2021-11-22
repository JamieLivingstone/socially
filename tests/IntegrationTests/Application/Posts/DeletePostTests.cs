using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Posts.Commands.DeletePost;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;

namespace IntegrationTests.Application.Posts
{
  [TestFixture]
  public class DeletePostTests : TestBase
  {
    [Test]
    public void GivenPostDoesNotExist_ThrowsNotFoundException()
    {
      var command = new DeletePostCommand
      {
        Slug = "does-not-exist",
      };

      async Task Handler() => await SendAsync(command);

      Assert.ThrowsAsync(typeof(NotFoundException), Handler);
    }

    [Test]
    public void GivenPostAuthorIsNotCurrentUser_ThrowsForbiddenException()
    {
      var target = Seed.Posts().First(c => c.AuthorId != Seed.CurrentUserId);

      var command = new DeletePostCommand
      {
        Slug = target.Slug,
      };

      async Task Handler() => await SendAsync(command);

      Assert.ThrowsAsync(typeof(ForbiddenException), Handler);
    }

    [Test]
    public async Task GivenAValidRequest_DeletesPost()
    {
      var target = Seed.Posts().First(c => c.AuthorId == Seed.CurrentUserId);

      var command = new DeletePostCommand
      {
        Slug = target.Slug,
      };

      await SendAsync(command);
    }
  }
}
