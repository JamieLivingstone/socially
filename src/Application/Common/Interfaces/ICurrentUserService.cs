namespace Application.Common.Interfaces
{
  public interface ICurrentUserService
  {
    public int UserId { get; }

    bool IsAuthenticated { get; }
  }
}
