using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Commands.LikePost
{
  [Authorize]
  public class LikePostCommand : IRequest
  {
    public string Slug { get; init; }

    public class LikePostCommandHandler : IRequestHandler<LikePostCommand>
    {
      private readonly IApplicationDbContext _dbContext;
      private readonly ICurrentUserService _currentUserService;

      public LikePostCommandHandler(IApplicationDbContext dbContext,
        ICurrentUserService currentUserService)
      {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
      }

      public async Task<Unit> Handle(LikePostCommand request, CancellationToken cancellationToken)
      {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);

        if (post == null)
        {
          throw new NotFoundException("Post does not exist.");
        }

        var liked = await _dbContext.Likes.AnyAsync(l => l.ObserverId == _currentUserService.UserId && l.PostId == post.Id, cancellationToken);

        if (!liked)
        {
          var like = new Domain.Entities.Like
          {
            PostId = post.Id,
            ObserverId = _currentUserService.UserId,
          };

          await _dbContext.Likes.AddAsync(like, cancellationToken);
          await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
      }
    }
  }
}
