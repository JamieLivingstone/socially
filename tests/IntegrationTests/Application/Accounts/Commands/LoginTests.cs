using System.Threading.Tasks;
using Application.Accounts.Commands.Login;
using Application.Accounts.Commands.Register;
using Application.Common.Exceptions;
using FluentAssertions;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;

namespace IntegrationTests.Application.Accounts.Commands
{
  [TestFixture]
  public class LoginTests : TestBase
  {
    [Test]
    public void GivenAnInvalidCommand_ThrowsValidationException()
    {
      var command = new LoginCommand();

      FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public void GivenInvalidCredentials_ThrowsUnauthorizedException()
    {
      var command = new LoginCommand
      {
        Username = "invalid",
        Password = "invalid"
      };

      async Task Handler()
      {
        await SendAsync(command);
      }

      Assert.ThrowsAsync(typeof(UnauthorizedException), Handler);
    }

    [Test]
    public async Task GivenValidCredentials_ReturnsSignedJwt()
    {
      await SendAsync(new RegisterCommand
      {
        Name = "John Doe",
        Username = "john",
        Email = "john.doe@test.com",
        Password = "ComplexPassword@12345"
      });

      var response = await SendAsync(new LoginCommand
      {
        Username = "John",
        Password = "ComplexPassword@12345"
      });

      Assert.True(response.Token.StartsWith("eyJ")); // Signed JWTs JSON always start with {"alg" when base64 encoded starts with "eyJ"
    }
  }
}
