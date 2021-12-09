using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Comments;

[TestFixture]
public class GetComments : TestBase
{
  [Test]
  public async Task GivenValidationFails_ReturnsBadRequest()
  {
    var target = Seed.Posts().First();
    const int pageSize = int.MaxValue; // Exceeds page size constraint

    var response = await AnonymousClient.GetAsync($"/api/v1/posts/{target.Slug}/comments?currentPage=1&pageSize={pageSize}");

    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
  }

  [Test]
  public async Task GivenPostDoesNotExist_ReturnsNotFound()
  {
    var response = await AnonymousClient.GetAsync("/api/v1/posts/does-not-exist/comments");

    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
  }

  [Test]
  public async Task GivenAValidRequest_ReturnsOk()
  {
    var target = Seed.Posts().First();

    var response = await AnonymousClient.GetAsync($"/api/v1/posts/{target.Slug}/comments");

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
  }
}
