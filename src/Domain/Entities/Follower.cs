namespace Domain.Entities
{
  public class Follower
  {
    public int ObserverId { get; set; }

    public Person Observer { get; set; }

    public int TargetId { get; set; }

    public Person Target { get; set; }
  }
}
