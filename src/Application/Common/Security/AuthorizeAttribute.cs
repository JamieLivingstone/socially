using System;

namespace Application.Common.Security
{
  [AttributeUsage(AttributeTargets.Class)]
  public class AuthorizeAttribute : Attribute
  {
  }
}
