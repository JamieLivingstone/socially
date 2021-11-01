using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
  public interface IPasswordHasher
  {
    byte[] GenerateSalt();

    Task<byte[]> Hash(string password, byte[] salt);
  }
}
