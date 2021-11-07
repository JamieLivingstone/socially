using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts.Commands.Login
{
  public class LoginCommand : IRequest<LoginDto>
  {
    public string Username { get; init; }

    public string Password { get; init; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
    {
      private readonly IApplicationDbContext _dbContext;
      private readonly IJwtTokenGenerator _jwtTokenGenerator;
      private readonly IPasswordHasher _passwordHasher;

      public LoginCommandHandler(IApplicationDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
      {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
      }

      public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
      {
        var person = await _dbContext.Persons.Where(p => p.Username == request.Username.ToLowerInvariant()).FirstOrDefaultAsync(cancellationToken);

        if (person == null || !person.Hash.SequenceEqual(await _passwordHasher.Hash(request.Password, person.Salt)))
        {
          throw new UnauthorizedException("Invalid username or password combination.");
        }

        return new LoginDto
        {
          Token = _jwtTokenGenerator.CreateToken(person.Id),
        };
      }
    }
  }
}
