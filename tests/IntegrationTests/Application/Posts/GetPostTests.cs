using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Posts.Queries.GetPost;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Posts;

[TestFixture]
public class GetPostTests : TestBase
{
  [Test]
  public void GivenPostDoesNotExist_ThrowsNotFoundException()
  {
    var query = new GetPostQuery
    {
      Slug = "does-not-exist",
    };

    async Task Handler() => await SendAsync(query);

    Assert.ThrowsAsync(typeof(NotFoundException), Handler);
  }

  [Test]
  public async Task GivenPostExists_ReturnsPost()
  {
    var query = new GetPostQuery
    {
      Slug = Seed.Posts().First().Slug,
    };

    var post = await SendAsync(query);

    Snapshot.Match(post);
  }
}
