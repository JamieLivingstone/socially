namespace Domain.Entities
{
  public class Person
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public byte[] Hash { get; set; }

    public byte[] Salt { get; set; }
  }
}
