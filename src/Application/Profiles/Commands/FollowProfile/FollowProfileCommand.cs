using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Commands.FollowProfile
{
  [Authorize]
  public class FollowProfileCommand : IRequest
  {
    public string Username { get; init; }

    public class FollowProfileCommandHandler : IRequestHandler<FollowProfileCommand>
    {
      private readonly IApplicationDbContext _dbContext;
      private readonly ICurrentUserService _currentUserService;

      public FollowProfileCommandHandler(IApplicationDbContext dbContext,
        ICurrentUserService currentUserService)
      {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
      }

      public async Task<Unit> Handle(FollowProfileCommand request, CancellationToken cancellationToken)
      {
        var target = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Username == request.Username.ToLowerInvariant(), cancellationToken);

        if (target == null)
        {
          throw new NotFoundException($"{request.Username} is not registered.");
        }

        if (target.Id == _currentUserService.UserId)
        {
          throw new UnprocessableEntityException("Cannot follow self.");
        }

        var isFollowing = await _dbContext.Followers.AnyAsync(f => f.ObserverId == _currentUserService.UserId && f.TargetId == target.Id, cancellationToken);

        if (!isFollowing)
        {
          var follower = new Follower
          {
            TargetId = target.Id,
            ObserverId = _currentUserService.UserId,
          };

          await _dbContext.Followers.AddAsync(follower, cancellationToken);
          await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
      }
    }
  }
}
