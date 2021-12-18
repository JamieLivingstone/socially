using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Comments.Queries.GetCommentsWithPagination;

public class GetCommentsWithPaginationQuery : IRequest<PaginatedList<CommentDto>>
{
  public string Slug { get; init; }

  public int PageNumber { get; init; } = 1;

  public int PageSize { get; init; } = 10;
}

public class GetCommentsWithPaginationQueryHandler : IRequestHandler<GetCommentsWithPaginationQuery, PaginatedList<CommentDto>>
{
  private readonly IApplicationDbContext _dbContext;
  private readonly IMapper _mapper;

  public GetCommentsWithPaginationQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
  {
    _dbContext = dbContext;
    _mapper = mapper;
  }

  public async Task<PaginatedList<CommentDto>> Handle(GetCommentsWithPaginationQuery request, CancellationToken cancellationToken)
  {
    var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);

    if (post == null)
    {
      throw new NotFoundException("Post does not exist.");
    }

    return await _dbContext.Comments
      .Where(c => c.PostId == post.Id)
      .OrderByDescending(c => c.CreatedAt)
      .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
      .PaginatedListAsync(request.PageNumber, request.PageSize);
  }
}
