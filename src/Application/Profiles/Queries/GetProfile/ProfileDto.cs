using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles.Queries.GetProfile;

public class ProfileDto : IMapFrom<Person>
{
  public string Name { get; init; }

  public string Username { get; init; }

  public int CommentsCount { get; set; }

  public int FollowersCount { get; set; }

  public int PostsCount { get; set; }

  public bool Following { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Person, ProfileDto>();
  }
}
