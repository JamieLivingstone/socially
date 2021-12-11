using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

  public string[] Tags { get; init; }

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

      var tags = await CreateTags(request.Tags ?? Array.Empty<string>(), cancellationToken);

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

      await _dbContext.PostTags.AddRangeAsync(tags.Select(x => new PostTag
      {
        Post = post,
        Tag = x
      }), cancellationToken);

      await _dbContext.SaveChangesAsync(cancellationToken);

      return new CreatePostDto
      {
        Slug = post.Slug
      };
    }

    private async Task<List<Tag>> CreateTags(IEnumerable<string> tags, CancellationToken cancellationToken)
    {
      var tagList = new List<Tag>();

      foreach (var tag in tags)
      {
        var tagId = tag.ToLowerInvariant();

        var t = await _dbContext.Tags.FindAsync(new object[]
        {
          tagId
        }, cancellationToken);

        if (t == null)
        {
          t = new Tag
          {
            TagId = tagId
          };
          await _dbContext.Tags.AddAsync(t, cancellationToken);
          await _dbContext.SaveChangesAsync(cancellationToken);
        }

        tagList.Add(t);
      }

      return tagList;
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
