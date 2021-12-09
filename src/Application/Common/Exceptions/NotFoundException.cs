using System;

namespace Application.Common.Exceptions;

public class NotFoundException : Exception
{
  public NotFoundException()
  {
  }

  public NotFoundException(string message) : base(message)
  {
  }
}
