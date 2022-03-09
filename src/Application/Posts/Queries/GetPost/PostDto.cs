using System;
using System.Collections.Generic;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Posts.Queries.GetPost;

public class PostDto : IMapFrom<Post>
{
  public string Slug { get; set; }

  public string Title { get; set; }

  public string Body { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public AuthorDto Author { get; set; }

  public IEnumerable<PostTagDto> Tags { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Post, PostDto>()
      .ForMember(d => d.Author, opt => opt.MapFrom(s => s.Author))
      .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.PostTags));
  }
}

public class AuthorDto : IMapFrom<Person>
{
  public string Name { get; set; }

  public string Username { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Person, AuthorDto>();
  }
}

public class PostTagDto : IMapFrom<PostTag>
{
  public string Name { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<PostTag, PostTagDto>()
      .ForMember(d => d.Name, opt => opt.MapFrom(s => s.TagId));
  }
}
