using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Accounts.Commands.Register;
using FunctionalTests.Api.TestUtils;
using NUnit.Framework;

namespace FunctionalTests.Api.V1.Accounts;

[TestFixture]
public class Register : TestBase
{
  [Test]
  public async Task GivenValidationFails_ReturnsBadRequest()
  {
    var command = new RegisterCommand();

    var response = await AnonymousClient.PostAsJsonAsync("/api/v1/accounts/register", command);

    Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
  }

  [Test]
  public async Task GivenValidRequest_ReturnsCreated()
  {
    var command = new RegisterCommand
    {
      Name = "John Doe",
      Username = "john",
      Email = "john.doe@test.com",
      Password = "Password@123"
    };

    var response = await AnonymousClient.PostAsJsonAsync("/api/v1/accounts/register", command);

    Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
  }
}
