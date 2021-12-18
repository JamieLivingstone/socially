using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Profiles.Queries.GetProfile;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Profiles;

public class GetProfileTests : TestBase
{
  [Test]
  public void GivenProfileDoesNotExist_ThrowsNotFoundException()
  {
    var query = new GetProfileQuery
    {
      Username = "does-not-exist",
    };

    async Task Handler() => await SendAsync(query);

    Assert.ThrowsAsync(typeof(NotFoundException), Handler);
  }

  [Test]
  public async Task GivenProfileExists_ReturnsProfileDto()
  {
    var query = new GetProfileQuery
    {
      Username = Seed.Persons().First().Username,
    };

    var profile = await SendAsync(query);

    Snapshot.Match(profile);
  }
}
