using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Commands.CreateComment
{
  [Authorize]
  public class CreateCommentCommand : IRequest<CreateCommentCommandDto>
  {
    public string Message { get; init; }

    public string Slug { get; init; }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreateCommentCommandDto>
    {
      private readonly IApplicationDbContext _dbContext;
      private readonly ICurrentUserService _currentUserService;

      public CreateCommentCommandHandler(IApplicationDbContext dbContext,
        ICurrentUserService currentUserService)
      {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
      }

      public async Task<CreateCommentCommandDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
      {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);

        if (post == null)
        {
          throw new NotFoundException("Post does not exist.");
        }

        var comment = new Comment
        {
          AuthorId = _currentUserService.UserId,
          Message = request.Message,
          PostId = post.Id,
        };

        await _dbContext.Comments.AddAsync(comment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateCommentCommandDto
        {
          Id = comment.Id
        };
      }
    }
  }
}
