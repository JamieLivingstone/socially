using System.Threading.Tasks;
using Application.Accounts.Commands.Register;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
  [Route("api/v1/accounts/[action]")]
  public class AccountsController : BaseController
  {
    [HttpPost]
    [ProducesResponseType(typeof(RegisterVm), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
      var response = await Mediator.Send(command);

      return CreatedAtAction(nameof(Register), response);
    }
  }
}
