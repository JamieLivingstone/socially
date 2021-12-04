namespace Domain.Entities
{
  public class Like
  {
    public int ObserverId { get; set; }

    public Person Observer { get; set; }

    public int PostId { get; set; }

    public Post Post { get; set; }
  }
}
