using System.Linq;
using System.Threading.Tasks;
using Application.Comments.Queries.GetCommentsWithPagination;
using Application.Common.Exceptions;
using FluentAssertions;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;
using ValidationException = FluentValidation.ValidationException;

namespace IntegrationTests.Application.Comments;

[TestFixture]
public class GetCommentsWithPaginationTests : TestBase
{
  [Test]
  public void GivenAnInvalidRequest_ThrowsValidationException()
  {
    var query = new GetCommentsWithPaginationQuery
    {
      Slug = Seed.Posts().First().Slug,
      PageNumber = 0, // Must be 1 or greater
      PageSize = 10
    };

    FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
  }

  [Test]
  public void GivenPostDoesNotExist_ThrowsNotFoundException()
  {
    var query = new GetCommentsWithPaginationQuery
    {
      Slug = "does-not-exist",
      PageNumber = 1,
      PageSize = 10
    };

    async Task Handler()
    {
      await SendAsync(query);
    }

    Assert.ThrowsAsync(typeof(NotFoundException), Handler);
  }

  [Test]
  public async Task GivenAValidRequest_ReturnsPaginatedComments()
  {
    var pageOne = await SendAsync(new GetCommentsWithPaginationQuery
    {
      Slug = Seed.Posts().First().Slug,
      PageNumber = 1,
      PageSize = 3
    });

    var pageTwo = await SendAsync(new GetCommentsWithPaginationQuery
    {
      Slug = Seed.Posts().First().Slug,
      PageNumber = 2,
      PageSize = 3
    });

    Snapshot.Match(new
    {
      pageOne,
      pageTwo
    });
  }
}
