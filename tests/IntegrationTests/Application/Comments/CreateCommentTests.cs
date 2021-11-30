using System.Linq;
using System.Threading.Tasks;
using Application.Comments.Commands.CreateComment;
using Application.Common.Exceptions;
using Domain.Entities;
using FluentAssertions;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Comments
{
  [TestFixture]
  public class CreateCommentTests : TestBase
  {
    [Test]
    public void GivenAnInvalidRequest_ThrowsValidationException()
    {
      var command = new CreateCommentCommand
      {
        Message = "", // Required field
        Slug = Seed.Posts().First().Slug
      };

      FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public void GivenPostDoesNotExist_ThrowsNotFoundException()
    {
      var command = new CreateCommentCommand
      {
        Message = "Test comment",
        Slug = "does-not-exist"
      };

      async Task Handler() => await SendAsync(command);

      Assert.ThrowsAsync(typeof(NotFoundException), Handler);
    }

    [Test]
    public async Task GivenAValidRequest_CreatesComment()
    {
      var command = new CreateCommentCommand
      {
        Message = "Test comment",
        Slug = Seed.Posts().First().Slug,
      };

      var result = await SendAsync(command);
      var comment = await FindByIdAsync<Comment>(result.Id);

      Snapshot.Match(comment, options => options.IgnoreField("Id"));
    }
  }
}
