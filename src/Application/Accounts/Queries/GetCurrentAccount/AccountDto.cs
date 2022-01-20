using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Accounts.Queries.GetCurrentAccount;

public class AccountDto : IMapFrom<Person>
{
  public string Name { get; set; }

  public string Username { get; set; }

  public string Email { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Person, AccountDto>();
  }
}
