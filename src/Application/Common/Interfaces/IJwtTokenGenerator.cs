namespace Application.Common.Interfaces
{
  public interface IJwtTokenGenerator
  {
    public string CreateToken(int userId);
  }
}
