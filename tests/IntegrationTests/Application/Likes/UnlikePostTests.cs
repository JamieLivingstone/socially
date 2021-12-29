using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Likes.Commands.LikePost;
using Application.Likes.Commands.UnlikePost;
using Domain.Entities;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;

namespace IntegrationTests.Application.Likes;

[TestFixture]
public class UnlikePostTests : TestBase
{
  [Test]
  public void GivenPostDoesNotExist_ThrowsNotFoundException()
  {
    var command = new UnlikePostCommand
    {
      Slug = "does-not-exist"
    };

    async Task Handler() => await SendAsync(command);

    Assert.ThrowsAsync(typeof(NotFoundException), Handler);
  }

  [Test]
  public async Task GivenAValidRequest_UnlikesPost()
  {
    var target = Seed.Posts().First();

    await SendAsync(new LikePostCommand
    {
      Slug = target.Slug
    });

    Assert.NotNull(await FindAsync<Like>(l => l.ObserverId == Seed.CurrentUserId && l.PostId == target.Id));

    await SendAsync(new UnlikePostCommand
    {
      Slug = target.Slug
    });

    Assert.Null(await FindAsync<Like>(l => l.ObserverId == Seed.CurrentUserId && l.PostId == target.Id));
  }
}
