using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Profiles.Commands.FollowProfile;
using Application.Profiles.Commands.UnfollowProfile;
using Domain.Entities;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;

namespace IntegrationTests.Application.Profiles;

[TestFixture]
public class UnfollowProfileTests : TestBase
{
  [Test]
  public void GivenProfileDoesNotExist_ThrowsNotFoundException()
  {
    var command = new UnfollowProfileCommand
    {
      Username = "does_not_exist"
    };

    async Task Handler()
    {
      await SendAsync(command);
    }

    Assert.ThrowsAsync(typeof(NotFoundException), Handler);
  }

  [Test]
  public async Task GivenValidRequest_UnfollowsProfile()
  {
    var target = Seed.Persons().First(p => p.Id != Seed.CurrentUserId);

    await SendAsync(new FollowProfileCommand
    {
      Username = target.Username
    });

    Assert.NotNull(await FindByIdAsync<Follower>(Seed.CurrentUserId, target.Id));

    await SendAsync(new UnfollowProfileCommand
    {
      Username = target.Username
    });

    Assert.Null(await FindByIdAsync<Follower>(Seed.CurrentUserId, target.Id));
  }
}
