using Application.Common.Interfaces;

namespace IntegrationTests.Application.TestUtils;

public class MockCurrentUserService : ICurrentUserService
{
  public int UserId => 1;

  public bool IsAuthenticated => true;
}
