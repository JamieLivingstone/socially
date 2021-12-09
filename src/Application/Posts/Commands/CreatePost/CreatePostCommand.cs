using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Slugify;

namespace Application.Posts.Commands.CreatePost;

[Authorize]
public class CreatePostCommand : IRequest<CreatePostDto>
{
  public string Title { get; init; }

  public string Body { get; init; }

  public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatePostDto>
  {
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public CreatePostCommandHandler(IApplicationDbContext dbContext,
      ICurrentUserService currentUserService)
    {
      _dbContext = dbContext;
      _currentUserService = currentUserService;
    }

    public async Task<CreatePostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
      var now = DateTime.UtcNow;

      var post = new Post
      {
        Slug = await GenerateUniqueSlug(request.Title, cancellationToken),
        Title = request.Title,
        Body = request.Body,
        CreatedAt = now,
        UpdatedAt = now,
        AuthorId = _currentUserService.UserId
      };

      await _dbContext.Posts.AddAsync(post, cancellationToken);

      await _dbContext.SaveChangesAsync(cancellationToken);

      return new CreatePostDto
      {
        Slug = post.Slug
      };
    }

    private async Task<string> GenerateUniqueSlug(string title, CancellationToken cancellationToken)
    {
      var slug = new SlugHelper().GenerateSlug(title);

      var isCollision = await _dbContext.Posts.AnyAsync(p => p.Slug == slug, cancellationToken);

      if (isCollision)
      {
        slug += $"-{Path.GetRandomFileName().Replace(".", "")}";
      }

      return slug;
    }
  }
}
