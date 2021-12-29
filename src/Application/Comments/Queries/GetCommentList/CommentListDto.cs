using System;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Comments.Queries.GetCommentList;

public class CommentListDto : IMapFrom<Comment>
{
  public int Id { get; init; }

  public string Message { get; init; }

  public DateTime CreatedAt { get; init; }

  public Author Author { get; init; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Comment, CommentListDto>()
      .ForMember(d => d.Author, opt => opt.MapFrom(s => s.Author));
  }
}

public class Author : IMapFrom<Person>
{
  public string Username { get; init; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Person, Author>();
  }
}
