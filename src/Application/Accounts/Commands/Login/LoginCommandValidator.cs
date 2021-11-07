using FluentValidation;

namespace Application.Accounts.Commands.Login
{
  public class LoginCommandValidator : AbstractValidator<LoginCommand>
  {
    public LoginCommandValidator()
    {
      RuleFor(x => x.Username)
        .NotNull()
        .NotEmpty();

      RuleFor(x => x.Password)
        .NotNull()
        .NotEmpty();
    }
  }
}
