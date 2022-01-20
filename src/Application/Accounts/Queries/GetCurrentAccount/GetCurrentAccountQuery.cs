using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Security;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts.Queries.GetCurrentAccount;

[Authorize]
public class GetCurrentAccountQuery : IRequest<AccountDto>
{
}

public class GetCurrentAccountQueryHandler : IRequestHandler<GetCurrentAccountQuery, AccountDto>
{
  private readonly IApplicationDbContext _dbContext;
  private readonly ICurrentUserService _currentUserService;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IMapper _mapper;

  public GetCurrentAccountQueryHandler(IApplicationDbContext dbContext,
    ICurrentUserService currentUserService,
    IJwtTokenGenerator jwtTokenGenerator,
    IMapper mapper)
  {
    _dbContext = dbContext;
    _currentUserService = currentUserService;
    _jwtTokenGenerator = jwtTokenGenerator;
    _mapper = mapper;
  }

  public async Task<AccountDto> Handle(GetCurrentAccountQuery request, CancellationToken cancellationToken)
  {
    var person = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == _currentUserService.UserId, cancellationToken);

    return _mapper.Map<Person, AccountDto>(person);
  }
}
