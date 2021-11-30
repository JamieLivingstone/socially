using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Commands.DeleteComment
{
  [Authorize]
  public class DeleteCommentCommand : IRequest
  {
    public int CommentId { get; init; }

    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
      private readonly IApplicationDbContext _dbContext;
      private readonly ICurrentUserService _currentUserService;

      public DeleteCommentCommandHandler(IApplicationDbContext dbContext,
        ICurrentUserService currentUserService)
      {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
      }

      public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
      {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);

        if (comment == null)
        {
          throw new NotFoundException("Comment does not exist.");
        }

        if (comment.AuthorId != _currentUserService.UserId)
        {
          throw new ForbiddenException("You do not have access to delete the specified comment.");
        }

        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
      }
    }
  }
}
