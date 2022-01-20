using System.Threading.Tasks;
using Application.Accounts.Queries.GetCurrentAccount;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Accounts.Queries;

[TestFixture]
public class GetCurrentAccountTests : TestBase
{
  [Test]
  public async Task GivenAnAuthenticatedRequest_ReturnsAccount()
  {
    var query = new GetCurrentAccountQuery();

    var account = await SendAsync(query);

    Snapshot.Match(account);
  }
}
