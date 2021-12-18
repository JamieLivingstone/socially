using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Posts.Commands.UpdatePost;
using Domain.Entities;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Posts;

[TestFixture]
public class UpdatePostTests : TestBase
{
  [Test]
  public void GivenPostDoesNotExist_ThrowsNotFoundException()
  {
    var command = new UpdatePostCommand
    {
      Slug = "does-not-exist"
    };

    async Task Handler() => await SendAsync(command);

    Assert.ThrowsAsync(typeof(NotFoundException), Handler);
  }

  [Test]
  public void GivenPostAuthorIsNotCurrentUser_ThrowsForbiddenException()
  {
    var target = Seed.Posts().First(c => c.AuthorId != Seed.CurrentUserId);

    var command = new UpdatePostCommand
    {
      Slug = target.Slug
    };

    async Task Handler() => await SendAsync(command);

    Assert.ThrowsAsync(typeof(ForbiddenException), Handler);
  }

  [Test]
  public async Task GivenAValidRequest_UpdatesPost()
  {
    var target = Seed.Posts().First(c => c.AuthorId == Seed.CurrentUserId);

    var command = new UpdatePostCommand
    {
      Slug = target.Slug,
      Title = "Updated title",
      Tags = new []{ "programming" }
    };

    await SendAsync(command);

    var updatedPost = await FindAsync<Post>(p => p.Slug == command.Slug, true);

    Snapshot.Match(updatedPost, options =>
    {
      options.IgnoreField("Id");
      options.IgnoreField("CreatedAt");
      options.IgnoreField("UpdatedAt");
      options.IgnoreField("Author");

      return options;
    });
  }
}
