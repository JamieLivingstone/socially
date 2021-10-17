using System;
using System.Collections.Generic;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters
{
  public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
  {
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
      _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
      {
        { typeof(ForbiddenException), HandleForbiddenException },
        { typeof(NotFoundException), HandleNotFoundException },
        { typeof(UnauthorizedException), HandleUnauthorizedAccessException },
        { typeof(UnprocessableEntityException), HandleUnprocessableEntityException },
        { typeof(ValidationException), HandleValidationException }
      };
    }

    public override void OnException(ExceptionContext context)
    {
      HandleException(context);

      base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
      var type = context.Exception.GetType();

      if (_exceptionHandlers.ContainsKey(type))
      {
        _exceptionHandlers[type].Invoke(context);
        return;
      }

      if (!context.ModelState.IsValid)
      {
        HandleInvalidModelStateException(context);
        return;
      }

      HandleUnknownException(context);
    }

    private static void HandleInvalidModelStateException(ExceptionContext context)
    {
      var details = new ValidationProblemDetails(context.ModelState)
      {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
      };

      context.Result = new BadRequestObjectResult(details);

      context.ExceptionHandled = true;
    }

    private static void HandleForbiddenException(ExceptionContext context)
    {
      var details = new ProblemDetails
      {
        Status = StatusCodes.Status403Forbidden,
        Title = "Forbidden.",
        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3"
      };

      context.Result = new ObjectResult(details)
      {
        StatusCode = StatusCodes.Status403Forbidden
      };

      context.ExceptionHandled = true;
    }

    private static void HandleNotFoundException(ExceptionContext context)
    {
      var exception = context.Exception as NotFoundException;

      var details = new ProblemDetails
      {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
        Title = "The specified resource was not found.",
        Detail = exception?.Message
      };

      context.Result = new NotFoundObjectResult(details);

      context.ExceptionHandled = true;
    }

    private static void HandleUnauthorizedAccessException(ExceptionContext context)
    {
      var details = new ProblemDetails
      {
        Status = StatusCodes.Status401Unauthorized,
        Title = "Unauthorized.",
        Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
      };

      context.Result = new ObjectResult(details)
      {
        StatusCode = StatusCodes.Status401Unauthorized
      };

      context.ExceptionHandled = true;
    }

    private static void HandleUnprocessableEntityException(ExceptionContext context)
    {
      var details = new ProblemDetails
      {
        Status = StatusCodes.Status422UnprocessableEntity,
        Title = "Unprocessable Entity.",
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
      };

      context.Result = new ObjectResult(details)
      {
        StatusCode = StatusCodes.Status422UnprocessableEntity
      };

      context.ExceptionHandled = true;
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
      var details = new ProblemDetails
      {
        Status = StatusCodes.Status500InternalServerError,
        Title = "An error occurred while processing your request.",
        Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
      };

      context.Result = new ObjectResult(details)
      {
        StatusCode = StatusCodes.Status500InternalServerError
      };

      context.ExceptionHandled = true;
    }

    private static void HandleValidationException(ExceptionContext context)
    {
      var exception = context.Exception as ValidationException;

      var details = new ValidationProblemDetails(exception?.Errors)
      {
        Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
      };

      context.Result = new BadRequestObjectResult(details);

      context.ExceptionHandled = true;
    }
  }
}
