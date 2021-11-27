namespace Domain.Entities
{
  public class Comment
  {
    public int Id { get; set; }

    public string Message { get; set; }

    public int AuthorId { get; set; }

    public Person Author { get; set; }

    public int PostId { get; set; }

    public Post Post { get; set; }
  }
}
