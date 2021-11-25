using System;

namespace Domain.Entities
{
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
  }
}