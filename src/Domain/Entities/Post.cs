using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Post
{
  public int Id { get; set; }

  public string Slug { get; set; }

  public string Title { get; set; }

  public string Body { get; set; }

  public DateTime CreatedAt { get; set; }

  public DateTime UpdatedAt { get; set; }

  public int AuthorId { get; set; }

  public Person Author { get; set; }

  public IEnumerable<Comment> Comments { get; set; }

  public IEnumerable<Like> Likes { get; set; }

  public IEnumerable<PostTag> PostTags { get; set; }
}
