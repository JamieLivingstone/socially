using System;
using System.Collections.Generic;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Posts.Queries.GetPostList;

public class PostListDto : IMapFrom<Post>
{
  public string Slug { get; set; }

  public string Title { get; set; }

  public string Body { get; set; }

  public DateTime CreatedAt { get; set; }

  public AuthorDto Author { get; set; }

  public IEnumerable<PostTagDto> Tags { get; set; }

  public int CommentsCount { get; set; }

  public int LikesCount { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Post, PostListDto>()
      .ForMember(d => d.Author, opt => opt.MapFrom(s => s.Author))
      .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.PostTags));
  }
}

public class AuthorDto : IMapFrom<Person>
{
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
