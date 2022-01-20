using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Commands.UnfollowProfile;

[Authorize]
public class UnfollowProfileCommand : IRequest
{
  public string Username { get; init; }
}

public class UnfollowProfileCommandHandler : IRequestHandler<UnfollowProfileCommand>
{
  private readonly IApplicationDbContext _dbContext;
  private readonly ICurrentUserService _currentUserService;

  public UnfollowProfileCommandHandler(IApplicationDbContext dbContext,
    ICurrentUserService currentUserService)
  {
    _dbContext = dbContext;
    _currentUserService = currentUserService;
  }

  public async Task<Unit> Handle(UnfollowProfileCommand request, CancellationToken cancellationToken)
  {
    var target = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Username == request.Username.ToLowerInvariant(), cancellationToken);

    if (target == null)
    {
      throw new NotFoundException($"{request.Username} is not registered");
    }

    var follower = await _dbContext.Followers.FirstOrDefaultAsync(f => f.ObserverId == _currentUserService.UserId && f.TargetId == target.Id, cancellationToken);

    if (follower != null)
    {
      _dbContext.Followers.Remove(follower);
      await _dbContext.SaveChangesAsync(cancellationToken);
    }

    return Unit.Value;
  }
}
