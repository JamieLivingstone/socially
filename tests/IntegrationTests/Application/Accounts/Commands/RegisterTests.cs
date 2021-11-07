using System.Threading.Tasks;
using Application.Accounts.Commands.Register;
using Application.Common.Exceptions;
using FluentAssertions;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;

namespace IntegrationTests.Application.Accounts.Commands
{
  [TestFixture]
  public class RegisterTests : TestBase
  {
    [Test]
    public void GivenAnInvalidCommand_ThrowsValidationException()
    {
      var command = new RegisterCommand();

      FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task GivenAValidCommand_ReturnsSignedJwt()
    {
      var command = new RegisterCommand
      {
        Name = "John Doe",
        Username = "John",
        Email = "John.Doe@test.com",
        Password = "ComplexPassword@123"
      };

      var response = await SendAsync(command);

      Assert.True(response.Token.StartsWith("eyJ")); // Signed JWTs JSON always start with {"alg" when base64 encoded starts with "eyJ"
    }
  }
}
