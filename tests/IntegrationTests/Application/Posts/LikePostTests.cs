using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Posts.Commands.LikePost;
using Domain.Entities;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Posts
{
  [TestFixture]
  public class LikePostTests : TestBase
  {
    [Test]
    public void GivenPostDoesNotExist_ThrowsNotFoundException()
    {
      var command = new LikePostCommand
      {
        Slug = "does-not-exist",
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

      Snapshot.Match(await FindByIdAsync<Like>(Seed.CurrentUserId, target.Id));
    }
  }
}
