using System.Threading.Tasks;
using Application.Profiles.Commands.FollowProfile;
using Application.Profiles.Commands.UnfollowProfile;
using Application.Profiles.Queries.GetProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/v1/profiles/{username}")]
public class ProfilesController : BaseController
{
  [HttpGet]
  [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetProfile(string username)
  {
    var query = new GetProfileQuery
    {
      Username = username
    };

    var response = await Mediator.Send(query);

    return Ok(response);
  }

  [HttpPost("followers")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
  public async Task<IActionResult> FollowProfile(string username)
  {
    var command = new FollowProfileCommand
    {
      Username = username
    };

    var response = await Mediator.Send(command);

    return CreatedAtAction(nameof(FollowProfile), response);
  }

  [HttpDelete("followers")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> UnfollowProfile(string username)
  {
    var command = new UnfollowProfileCommand
    {
      Username = username
    };

    var response = await Mediator.Send(command);

    return Ok(response);
  }
}
