using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;

namespace Application.Common.Behaviours
{
  public class RequestAuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {
    private readonly ICurrentUserService _currentUserService;

    public RequestAuthorizationBehaviour(ICurrentUserService currentUserService)
    {
      _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
      var shouldAuthorize = request.GetType().GetCustomAttribute<AuthorizeAttribute>();

      if (shouldAuthorize != null && _currentUserService.IsAuthenticated == false)
      {
        throw new UnauthorizedException("Authorization failed.");
      }

      return await next();
    }
  }
}
