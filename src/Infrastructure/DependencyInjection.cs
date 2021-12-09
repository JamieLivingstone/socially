using System;
using System.Text;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Security;
using Infrastructure.Security.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    ConfigureJwt(services, configuration);

    services.AddDbContext<ApplicationDbContext>(options =>
    {
      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
        b => { b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName); });
    });

    services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

    services.Configure<PasswordHasherOptions>(configuration.GetSection("Security:PasswordHasherOptions"));

    services.AddTransient<IPasswordHasher, PasswordHasher>();

    services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
  }

  private static void ConfigureJwt(IServiceCollection services, IConfiguration configuration)
  {
    var issuer = configuration["Security:JwtIssuerOptions:Issuer"];
    var audience = configuration["Security:JwtIssuerOptions:Audience"];
    var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Security:JwtIssuerOptions:Key"]));
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

    services.Configure<JwtIssuerOptions>(options =>
    {
      options.Issuer = issuer;
      options.Audience = audience;
      options.SigningCredentials = signingCredentials;
    });

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ClockSkew = TimeSpan.Zero,
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = issuer,
          ValidAudience = audience,
          IssuerSigningKey = signingCredentials.Key
        };
      });
  }
}
