using System;

namespace Application.Common.Exceptions;

public class UnprocessableEntityException : Exception
{
  public UnprocessableEntityException(string message) : base(message)
  {
  }
}
