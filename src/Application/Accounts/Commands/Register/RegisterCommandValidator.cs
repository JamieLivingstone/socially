using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts.Commands.Register
{
  public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
  {
    private readonly IApplicationDbContext _dbContext;

    public RegisterCommandValidator(IApplicationDbContext dbContext)
    {
      _dbContext = dbContext;

      RuleFor(x => x.Name)
        .NotNull()
        .NotEmpty();

      RuleFor(x => x.Username)
        .NotNull()
        .NotEmpty()
        .Matches("^[a-zA-Z0-9_]*$").WithMessage("{PropertyName} must only contain letters, numbers and underscores.")
        .Must(NotContainWhitespace).WithMessage("{PropertyName} cannot contain spaces.")
        .MustAsync(BeUniqueUsername).WithMessage("{PropertyName} is already in use.");

      RuleFor(x => x.Email)
        .NotNull()
        .NotEmpty()
        .Must(NotContainWhitespace).WithMessage("{PropertyName} cannot contain spaces.")
        .EmailAddress()
        .MustAsync(BeUniqueEmail).WithMessage("{PropertyName} is already in use.");

      RuleFor(x => x.Password)
        .NotNull()
        .NotEmpty()
        .MinimumLength(8)
        .Matches("[a-z]").WithMessage("{PropertyName} must contain an lowercase letter.")
        .Matches("[A-Z]").WithMessage("{PropertyName} must contain an uppercase letter.");
    }

    private static bool NotContainWhitespace(string input)
    {
      return !(input ?? string.Empty).Any(char.IsWhiteSpace);
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
      return await _dbContext.Persons.AnyAsync(x => x.Email == (email ?? string.Empty).ToLowerInvariant(), cancellationToken) == false;
    }

    private async Task<bool> BeUniqueUsername(string userName, CancellationToken cancellationToken)
    {
      return await _dbContext.Persons.AnyAsync(x => x.Username == (userName ?? string.Empty).ToLowerInvariant(), cancellationToken) == false;
    }
  }
}
