using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Infrastructure.Security.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Security
{
  public class PasswordHasher : IPasswordHasher
  {
    private readonly PasswordHasherOptions _passwordHasherOptions;

    public PasswordHasher(IOptions<PasswordHasherOptions> options)
    {
      _passwordHasherOptions = options.Value;
    }

    public byte[] GenerateSalt()
    {
      var salt = new byte[128 / 8];
      using var rng = RandomNumberGenerator.Create();
      rng.GetBytes(salt);

      return salt;
    }

    public Task<byte[]> Hash(string password, byte[] salt)
    {
      var key = Encoding.UTF8.GetBytes(_passwordHasherOptions.SigningKey);
      var bytes = Encoding.UTF8.GetBytes(password);

      var allBytes = new byte[bytes.Length + salt.Length];
      Buffer.BlockCopy(bytes, 0, allBytes, 0, bytes.Length);
      Buffer.BlockCopy(salt, 0, allBytes, bytes.Length, salt.Length);

      return new HMACSHA512(key).ComputeHashAsync(new MemoryStream(allBytes));
    }
  }
}
