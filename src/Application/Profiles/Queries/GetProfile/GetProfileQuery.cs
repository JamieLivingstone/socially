using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Profiles.Queries.GetProfile;

public class GetProfileQuery : IRequest<ProfileDto>
{
  public string Username { get; init; }
}

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ProfileDto>
{
  private readonly IApplicationDbContext _dbContext;
  private readonly IMapper _mapper;

  public GetProfileQueryHandler(IApplicationDbContext dbContext,
    IMapper mapper)
  {
    _dbContext = dbContext;
    _mapper = mapper;
  }

  public async Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
  {
    var profile = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Username == request.Username.ToLowerInvariant(), cancellationToken);

    if (profile == null)
    {
      throw new NotFoundException("Profile does not exist.");
    }

    var profileDto = _mapper.Map<Person, ProfileDto>(profile);
    profileDto.CommentsCount = await _dbContext.Comments.CountAsync(c => c.AuthorId == profile.Id, cancellationToken);
    profileDto.FollowersCount = await _dbContext.Followers.CountAsync(f => f.TargetId == profile.Id, cancellationToken);
    profileDto.PostsCount = await _dbContext.Posts.CountAsync(p => p.AuthorId == profile.Id, cancellationToken);

    return profileDto;
  }
}
