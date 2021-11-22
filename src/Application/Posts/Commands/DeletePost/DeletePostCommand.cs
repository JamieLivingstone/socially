using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Commands.DeletePost
{
  [Authorize]
  public class DeletePostCommand : IRequest
  {
    public string Slug { get; init; }

    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
      private readonly IApplicationDbContext _dbContext;
      private readonly ICurrentUserService _currentUserService;

      public DeletePostCommandHandler(IApplicationDbContext dbContext,
        ICurrentUserService currentUserService)
      {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
      }

      public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
      {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);

        if (post == null)
        {
          throw new NotFoundException("Post does not exist.");
        }

        if (post.AuthorId != _currentUserService.UserId)
        {
          throw new ForbiddenException("You do not have access to delete the specified post.");
        }

        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
      }
    }
  }
}
