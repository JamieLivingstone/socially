using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Tags.Queries.GetTagList;
using FluentAssertions;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Tags;

[TestFixture]
public class GetTagListTests : TestBase
{
  [Test]
  public void GivenAnInvalidRequest_ThrowsValidationException()
  {
    var query = new GetTagListQuery
    {
      PageSize = int.MaxValue // Exceeds page size constraint
    };

    FluentActions.Invoking(() => SendAsync(query)).Should().ThrowAsync<ValidationException>();
  }

  [Test]
  public async Task GivenAPaginatedRequest_ReturnsTagList()
  {
    var query = new GetTagListQuery
    {
      PageNumber = 1,
      PageSize = 2,
    };

    var result = await SendAsync(query);

    Snapshot.Match(result);
  }

  [Test]
  public async Task GivenAPaginatedRequestWithTerm_ReturnsTagList()
  {
    var query = new GetTagListQuery
    {
      PageNumber = 1,
      PageSize = 10,
      Term = Seed.Tags().First().TagId
    };

    var result = await SendAsync(query);

    Snapshot.Match(result);
  }
}
