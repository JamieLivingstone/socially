using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Posts.Queries.GetPostList;
using FluentAssertions;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Posts;

[TestFixture]
public class GetPostListTests : TestBase
{
  [Test]
  public void GivenAnInvalidRequest_ThrowsValidationException()
  {
    var query = new GetPostListQuery
    {
      PageNumber = 0, // Must be 1 or greater
      PageSize = 10
    };

    FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
  }

  [Test]
  public async Task GivenFiltersHasNoCorrespondingPosts_ReturnsEmptyList()
  {
    var query = new GetPostListQuery
    {
      PageNumber = 1,
      PageSize = 1,
      Author = "does_not_exist"
    };

    var result = await SendAsync(query);

    Snapshot.Match(result);
  }

  [Test]
  public async Task GivenAPaginatedRequestWithCustomOrdering_ReturnsPostList()
  {
    var query = new GetPostListQuery
    {
      PageNumber = 2,
      PageSize = 1,
      OrderBy = PostListOrder.Created
    };

    var result = await SendAsync(query);

    Snapshot.Match(result);
  }
}
