using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Profiles;

[TestFixture]
public class GetProfile : TestBase
{
  [Test]
  public async Task GivenProfileDoesNotExist_ReturnsNotFound()
  {
    const string username = "does-not-exist";

    var response = await AnonymousClient.GetAsync($"api/v1/profiles/{username}");

    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [Test]
  public async Task GivenProfileExists_ReturnsOk()
  {
    var username = Seed.Persons().Last().Username;

    var response = await AnonymousClient.GetAsync($"api/v1/profiles/{username}");

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
  }
}
