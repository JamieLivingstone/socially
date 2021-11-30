using System.Linq;
using System.Threading.Tasks;
using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Common.Exceptions;
using Domain.Entities;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;

namespace IntegrationTests.Application.Comments
{
  [TestFixture]
  public class DeleteCommentTests : TestBase
  {
    [Test]
    public void GivenCommentDoesNotExist_ThrowsNotFoundException()
    {
      var command = new DeleteCommentCommand
      {
        CommentId = -1,
      };

      async Task Handler() => await SendAsync(command);

      Assert.ThrowsAsync(typeof(NotFoundException), Handler);
    }

    [Test]
    public void GivenCommentAuthorIsNotCurrentUser_ThrowsForbiddenException()
    {
      var target = Seed.Comments().First(c => c.AuthorId != Seed.CurrentUserId);

      var command = new DeleteCommentCommand
      {
        CommentId = target.Id
      };

      async Task Handler() => await SendAsync(command);

      Assert.ThrowsAsync(typeof(ForbiddenException), Handler);
    }

    [Test]
    public async Task GivenAValidRequest_DeletesComment()
    {
      var target = Seed.Posts().First();

      var comment = await SendAsync(new CreateCommentCommand
      {
        Message = "message",
        Slug = target.Slug
      });

      Assert.NotNull(await FindByIdAsync<Comment>(comment.Id));

      await SendAsync(new DeleteCommentCommand
      {
        CommentId = comment.Id
      });

      Assert.Null(await FindByIdAsync<Comment>(comment.Id));
    }
  }
}
