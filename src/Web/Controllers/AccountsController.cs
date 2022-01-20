using System.Threading.Tasks;
using Application.Accounts.Commands.Login;
using Application.Accounts.Commands.Register;
using Application.Accounts.Queries.GetCurrentAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/v1/accounts/[action]")]
public class AccountsController : BaseController
{
  [HttpPost]
  [ProducesResponseType(typeof(RegisterDto), StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Register(RegisterCommand command)
  {
    var response = await Mediator.Send(command);

    return CreatedAtAction(nameof(Register), response);
  }

  [HttpPost]
  [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<IActionResult> Login(LoginCommand command)
  {
    var response = await Mediator.Send(command);

    return Ok(response);
  }

  [HttpGet]
  [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<IActionResult> Current()
  {
    var response = await Mediator.Send(new GetCurrentAccountQuery());

    return Ok(response);
  }
}
