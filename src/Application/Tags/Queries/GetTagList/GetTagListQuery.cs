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

namespace Application.Tags.Queries.GetTagList;

public enum TagListOrder
{
  [EnumMember(Value = "popularity")] Popularity,
}

public class GetTagListQuery : IRequest<PaginatedList<TagListDto>>
{
  public string Term { get; init; }

  public int PageNumber { get; init; }

  public TagListOrder OrderBy { get; init; }

  public int PageSize { get; init; }
}

public class GetTagListQueryHandler : IRequestHandler<GetTagListQuery, PaginatedList<TagListDto>>
{
  private readonly IApplicationDbContext _dbContext;
  private readonly IMapper _mapper;

  public GetTagListQueryHandler(IApplicationDbContext dbContext,
    IMapper mapper)
  {
    _dbContext = dbContext;
    _mapper = mapper;
  }

  public async Task<PaginatedList<TagListDto>> Handle(GetTagListQuery request, CancellationToken cancellationToken)
  {
    var queryable = _dbContext.Tags
      .OrderBy(t => t.TagId)
      .AsNoTracking();

    if (!string.IsNullOrEmpty(request.Term))
    {
      queryable = queryable.Where(t => t.TagId.StartsWith(request.Term.ToLowerInvariant()));
    }

    switch (request.OrderBy)
    {
      case TagListOrder.Popularity:
      default:
        queryable = queryable.OrderByDescending(x => x.PostTags.Count());
        break;
    }

    return await queryable
      .ProjectTo<TagListDto>(_mapper.ConfigurationProvider)
      .PaginatedListAsync(request.PageNumber, request.PageSize);
  }
}
