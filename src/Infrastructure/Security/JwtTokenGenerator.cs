using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Common.Interfaces;
using Infrastructure.Security.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly JwtIssuerOptions _jwtIssuerOptions;

  public JwtTokenGenerator(IOptions<JwtIssuerOptions> jwtOptions)
  {
    _jwtIssuerOptions = jwtOptions.Value;
  }

  public string CreateToken(int userId)
  {
    var claims = new[]
    {
      new Claim(JwtRegisteredClaimNames.NameId, userId.ToString()),
      new Claim(JwtRegisteredClaimNames.Jti, JwtIssuerOptions.JtiGenerator()),
      new Claim(JwtRegisteredClaimNames.Iat,
        new DateTimeOffset(JwtIssuerOptions.IssuedAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
    };

    var jwt = new JwtSecurityToken(
      _jwtIssuerOptions.Issuer,
      _jwtIssuerOptions.Audience,
      claims,
      JwtIssuerOptions.NotBefore,
      _jwtIssuerOptions.Expiration,
      _jwtIssuerOptions.SigningCredentials);

    return new JwtSecurityTokenHandler().WriteToken(jwt);
  }
}
