using System;
using System.Security.Claims;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Web.Services
{
  public class CurrentUserService : ICurrentUserService
  {
    public int UserId { get; }

    public bool IsAuthenticated { get; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
      try
      {
        var nameIdentifier = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        UserId = int.Parse(nameIdentifier ?? throw new Exception("Name identifier is null"));
        IsAuthenticated = true;
      }
      catch (Exception)
      {
        IsAuthenticated = false;
      }
    }
  }
}
