using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Posts;

[TestFixture]
public class GetPost : TestBase
{
  [Test]
  public async Task GivenPostDoesNotExist_ReturnsNotFound()
  {
    const string slug = "does-not-exist";

    var response = await AnonymousClient.GetAsync($"api/v1/posts/{slug}");

    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [Test]
  public async Task GivenPostExists_ReturnsOk()
  {
    var slug = Seed.Posts().First().Slug;

    var response = await AnonymousClient.GetAsync($"api/v1/posts/{slug}");

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
  }
}
