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

  public DateTime CreatedAt { get; set; }

  public PostListAuthorDto Author { get; set; }

  public IEnumerable<PostListTagDto> Tags { get; set; }

  public int CommentsCount { get; set; }

  public int LikesCount { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Post, PostListDto>()
      .ForMember(d => d.Author, opt => opt.MapFrom(s => s.Author))
      .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.PostTags));
  }
}

public class PostListAuthorDto : IMapFrom<Person>
{
  public string Name { get; set; }

  public string Username { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Person, PostListAuthorDto>();
  }
}

public class PostListTagDto : IMapFrom<PostTag>
{
  public string Name { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<PostTag, PostListTagDto>()
      .ForMember(d => d.Name, opt => opt.MapFrom(s => s.TagId));
  }
}
