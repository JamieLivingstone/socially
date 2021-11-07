using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Accounts.Commands.Login;
using Application.Accounts.Commands.Register;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1
{
  [TestFixture]
  public class Login
  {
    private HttpClient _client;

    [SetUp]
    public void SetUp()
    {
      var factory = new CustomWebApplicationFactory();
      _client = factory.GetAnonymousClient();
    }

    [Test]
    public async Task GivenValidationFails_ReturnsBadRequest()
    {
      var command = new LoginCommand();

      var response = await _client.PostAsJsonAsync("/api/v1/accounts/login", command);

      Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Test]
    public async Task GivenInvalidCredentials_ReturnsUnauthorized()
    {
      var command = new LoginCommand
      {
        Username = "invalid",
        Password = "invalid"
      };

      var response = await _client.PostAsJsonAsync("/api/v1/accounts/login", command);

      Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Test]
    public async Task GivenValidCredentials_ReturnsOk()
    {
      var registerResponse = await _client.PostAsJsonAsync("/api/v1/accounts/register", new RegisterCommand
      {
        Name = "John Doe",
        Username = "john",
        Email = "john.doe@test.com",
        Password = "Password@123"
      });

      Assert.AreEqual(HttpStatusCode.Created, registerResponse.StatusCode);

      var loginCommand = new LoginCommand
      {
        Username = "john",
        Password = "Password@123"
      };

      var loginResponse = await _client.PostAsJsonAsync("/api/v1/accounts/login", loginCommand);

      Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }
  }
}
