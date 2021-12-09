using System.Net.Http;
using NUnit.Framework;

namespace FunctionalTests.Api.TestUtils;

public class TestBase
{
  protected HttpClient AnonymousClient;
  protected HttpClient AuthenticatedClient;

  [SetUp]
  public void SetUp()
  {
    var factory = new CustomWebApplicationFactory();
    AnonymousClient = factory.GetAnonymousClient();
    AuthenticatedClient = factory.GetAuthenticatedClientAsync();
  }
}
