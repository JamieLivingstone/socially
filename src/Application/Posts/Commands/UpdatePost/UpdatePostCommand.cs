using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Commands.UpdatePost;

[Authorize]
public class UpdatePostCommand : IRequest
{
  public string Slug { get; init; }

  public string Title { get; init; }

  public string Body { get; init; }

  public string[] Tags { get; init; }

  public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
  {
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePostCommandHandler(IApplicationDbContext dbContext,
      ICurrentUserService currentUserService)
    {
      _dbContext = dbContext;
      _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
      var post = await _dbContext.Posts
        .Include(p => p.PostTags)
        .FirstOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);

      if (post == null)
      {
        throw new NotFoundException("Post does not exist.");
      }

      post.UpdatedAt = DateTime.UtcNow;

      if (post.AuthorId != _currentUserService.UserId)
      {
        throw new ForbiddenException("You do not have access to update the specified post.");
      }

      if (request.Title != null)
      {
        post.Title = request.Title;
      }

      if (request.Body != null)
      {
        post.Body = request.Body;
      }

      if (request.Tags != null)
      {
        var postTagsToCreate = request.Tags
          .Where(tag => post.PostTags.Any(pt => pt.TagId == tag) == false)
          .Select(tag => new PostTag
          {
            PostId = post.Id,
            Tag = new Tag { TagId = tag.ToLowerInvariant() },
            TagId = tag.ToLowerInvariant()
          })
          .ToList();

        var postTagsToDelete = post.PostTags.Where(pt => request.Tags.Any(tag => pt.TagId != tag) == false);

        _dbContext.PostTags.RemoveRange(postTagsToDelete);
        await _dbContext.Tags.AddRangeAsync(postTagsToCreate.Select(pt => pt.Tag), cancellationToken);
        await _dbContext.PostTags.AddRangeAsync(postTagsToCreate, cancellationToken);
      }

      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}
