using System;

namespace Application.Common.Exceptions
{
  public class UnauthorizedException : Exception
  {
    public UnauthorizedException()
    {
    }

    public UnauthorizedException(string message) : base(message)
    {
    }
  }
}
