using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Likes.Commands.LikePost;
using Domain.Entities;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Likes;

[TestFixture]
public class LikePostTests : TestBase
{
  [Test]
  public void GivenPostDoesNotExist_ThrowsNotFoundException()
  {
    var command = new LikePostCommand
    {
      Slug = "does-not-exist"
    };

    async Task Handler() => await SendAsync(command);

    Assert.ThrowsAsync(typeof(NotFoundException), Handler);
  }

  [Test]
  public async Task GivenAValidRequest_LikesPost()
  {
    var target = Seed.Posts().First();

    await SendAsync(new LikePostCommand
    {
      Slug = target.Slug
    });

    Snapshot.Match(await FindAsync<Like>(l => l.ObserverId == Seed.CurrentUserId && l.PostId == target.Id));
  }
}
