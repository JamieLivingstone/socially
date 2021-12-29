using System.Net;
using System.Threading.Tasks;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Posts;

[TestFixture]
public class GetPostList : TestBase
{
  [Test]
  public async Task GivenValidationFails_ReturnsBadRequest()
  {
    const int pageSize = int.MaxValue;

    var response = await AnonymousClient.GetAsync($"/api/v1/posts?currentPage=1&pageSize={pageSize}");

    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
  }

  [Test]
  public async Task GivenAValidRequest_ReturnsOk()
  {
    var response = await AnonymousClient.GetAsync("/api/v1/posts");

    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
  }
}
