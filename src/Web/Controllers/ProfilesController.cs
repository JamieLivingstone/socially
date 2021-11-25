using System.Threading.Tasks;
using Application.Profiles.Commands.FollowProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
  [Route("api/v1/profiles/{username}")]
  public class ProfilesController : BaseController
  {
    [HttpPost("followers")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> FollowProfile(string username)
    {
      var command = new FollowProfileCommand
      {
        Username = username,
      };

      var response = await Mediator.Send(command);

      return CreatedAtAction(nameof(FollowProfile), response);
    }
  }
}
