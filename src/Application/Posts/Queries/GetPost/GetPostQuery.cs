using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Posts.Queries.GetPost;

public class GetPostQuery : IRequest<PostDto>
{
  public string Slug { get; init; }
}

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostDto>
{
  private readonly ICurrentUserService _currentUserService;
  private readonly IApplicationDbContext _dbContext;
  private readonly IMapper _mapper;

  public GetPostQueryHandler(IApplicationDbContext dbContext,
    IMapper mapper,
    ICurrentUserService currentUserService)
  {
    _dbContext = dbContext;
    _mapper = mapper;
    _currentUserService = currentUserService;
  }

  public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
  {
    var post = await _dbContext.Posts
      .Include(p => p.Author)
      .Include(p => p.PostTags)
      .AsNoTracking()
      .FirstOrDefaultAsync(p => p.Slug == request.Slug, cancellationToken);

    if (post == null)
    {
      throw new NotFoundException("Post does not exist");
    }

    var postDto = _mapper.Map<Post, PostDto>(post);
    postDto.Liked = _currentUserService.IsAuthenticated && await _dbContext.Likes.AnyAsync(l => l.ObserverId == _currentUserService.UserId && l.PostId == post.Id, cancellationToken);

    return postDto;
  }
}
