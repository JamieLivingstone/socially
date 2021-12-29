using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Likes.Commands.UnlikePost;

[Authorize]
public class UnlikePostCommand : IRequest
{
  public string Slug { get; init; }
}

public class UnlikeCommandHandler : IRequestHandler<UnlikePostCommand>
{
  private readonly IApplicationDbContext _dbContext;
  private readonly ICurrentUserService _currentUserService;

  public UnlikeCommandHandler(IApplicationDbContext dbContext,
    ICurrentUserService currentUserService)
  {
    _dbContext = dbContext;
    _currentUserService = currentUserService;
  }

  public async Task<Unit> Handle(UnlikePostCommand request, CancellationToken cancellationToken)
  {
    var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);

    if (post == null)
    {
      throw new NotFoundException("Post does not exist.");
    }

    var like = await _dbContext.Likes.FirstOrDefaultAsync(l => l.ObserverId == _currentUserService.UserId && l.PostId == post.Id, cancellationToken);

    if (like != null)
    {
      _dbContext.Likes.Remove(like);
      await _dbContext.SaveChangesAsync(cancellationToken);
    }

    return Unit.Value;
  }
}
