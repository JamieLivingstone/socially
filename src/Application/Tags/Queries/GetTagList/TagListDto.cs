using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Tags.Queries.GetTagList;

public class TagListDto : IMapFrom<Tag>
{
  public string Name { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Tag, TagListDto>()
      .ForMember(d => d.Name, opt => opt.MapFrom(s => s.TagId));
  }
}
