using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Profiles.Commands.FollowProfile;
using Domain.Entities;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Profiles;

[TestFixture]
public class FollowProfileTests : TestBase
{
  [Test]
  public void GivenProfileDoesNotExist_ThrowsNotFoundException()
  {
    var command = new FollowProfileCommand
    {
      Username = "does_not_exist"
    };

    async Task Handler() => await SendAsync(command);

    Assert.ThrowsAsync(typeof(NotFoundException), Handler);
  }

  [Test]
  public void GivenTargetProfileIsCurrentUser_ThrowsUnprocessableEntityException()
  {
    var command = new FollowProfileCommand
    {
      Username = Seed.Persons().First(p => p.Id == Seed.CurrentUserId).Username
    };

    async Task Handler() => await SendAsync(command);

    Assert.ThrowsAsync(typeof(UnprocessableEntityException), Handler);
  }

  [Test]
  public async Task GivenValidRequest_FollowsProfile()
  {
    var target = Seed.Persons().First(p => p.Id != Seed.CurrentUserId);

    await SendAsync(new FollowProfileCommand
    {
      Username = Seed.Persons().First(p => p.Id != Seed.CurrentUserId).Username
    });

    var follower = await FindAsync<Follower>(f => f.ObserverId == Seed.CurrentUserId  && f.TargetId == target.Id);

    Snapshot.Match(follower);
  }
}
