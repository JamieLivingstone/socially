using FluentValidation;

namespace Application.Accounts.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
  public LoginCommandValidator()
  {
    RuleFor(x => x.Username)
      .NotNull().WithMessage("Username cannot be null")
      .NotEmpty().WithMessage("Username is a required field");

    RuleFor(x => x.Password)
      .NotNull()
      .NotEmpty();
  }
}
