using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Queries.GetPostList;

public enum PostListOrder
{
  [EnumMember(Value = "created")] Created,
  [EnumMember(Value = "commentsCount")] CommentsCount,
  [EnumMember(Value = "likesCount")] LikesCount
}

public class GetPostListQuery : IRequest<PaginatedList<PostListDto>>
{
  public int PageNumber { get; init; }

  public int PageSize { get; init; }

  public PostListOrder OrderBy { get; init; }

  public string Author { get; init; }

  public string Tag { get; init; }
}

public class GetPostListQueryHandler : IRequestHandler<GetPostListQuery, PaginatedList<PostListDto>>
{
  private readonly IApplicationDbContext _dbContext;
  private readonly IMapper _mapper;
  private readonly ICurrentUserService _currentUserService;

  public GetPostListQueryHandler(IApplicationDbContext dbContext,
    IMapper mapper,
    ICurrentUserService currentUserService)
  {
    _dbContext = dbContext;
    _mapper = mapper;
    _currentUserService = currentUserService;
  }

  public async Task<PaginatedList<PostListDto>> Handle(GetPostListQuery request, CancellationToken cancellationToken)
  {
    var queryable = _dbContext.Posts
      .Include(p => p.Author)
      .Include(p => p.PostTags)
      .AsNoTracking();

    if (!string.IsNullOrWhiteSpace(request.Author))
    {
      var author = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Username == request.Author, cancellationToken);

      if (author != null)
      {
        queryable = queryable.Where(p => p.Author == author);
      }
      else
      {
        return new EmptyPostList(request.PageNumber, request.PageSize);
      }
    }

    if (!string.IsNullOrWhiteSpace(request.Tag))
    {
      var tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.TagId == request.Tag, cancellationToken);

      if (tag != null)
      {
        queryable = queryable.Where(p => p.PostTags.Select(pt => pt.TagId).Contains(tag.TagId));
      }
      else
      {
        return new EmptyPostList(request.PageNumber, request.PageSize);
      }
    }

    switch (request.OrderBy)
    {
      case PostListOrder.Created:
      default:
        queryable = queryable.OrderByDescending(x => x.CreatedAt);
        break;
      case PostListOrder.CommentsCount:
        queryable = queryable.OrderByDescending(x => x.Comments.Count());
        break;
      case PostListOrder.LikesCount:
        queryable = queryable.OrderByDescending(x => x.Likes.Count());
        break;
    }

    return await queryable
      .ProjectTo<PostListDto>(_mapper.ConfigurationProvider)
      .PaginatedListAsync(request.PageNumber, request.PageSize);
  }
}

public class EmptyPostList : PaginatedList<PostListDto>
{
  public EmptyPostList(int pageNumber, int pageSize) : base(new List<PostListDto>(), 0, pageNumber, pageSize)
  {
  }
}
