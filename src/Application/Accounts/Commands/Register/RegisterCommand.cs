using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Accounts.Commands.Register
{
  public class RegisterCommand : IRequest<RegisterVm>
  {
    public string Name { get; init; }

    public string Username { get; init; }

    public string Email { get; init; }

    public string Password { get; init; }

    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterVm>
    {
      private readonly IApplicationDbContext _dbContext;
      private readonly IJwtTokenGenerator _jwtTokenGenerator;
      private readonly IPasswordHasher _passwordHasher;

      public RegisterCommandHandler(IApplicationDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
      {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
      }

      public async Task<RegisterVm> Handle(RegisterCommand request, CancellationToken cancellationToken)
      {
        var salt = _passwordHasher.GenerateSalt();

        var person = new Person
        {
          Name = request.Name,
          Username = request.Username.ToLowerInvariant(),
          Email = request.Email.ToLowerInvariant(),
          Salt = salt,
          Hash = await _passwordHasher.Hash(request.Password, salt)
        };

        await _dbContext.Persons.AddAsync(person, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenGenerator.CreateToken(person.Id);

        return new RegisterVm
        {
          Token = token
        };
      }
    }
  }
}
