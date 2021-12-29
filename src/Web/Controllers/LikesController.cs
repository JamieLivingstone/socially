using System.Threading.Tasks;
using Application.Likes.Commands.LikePost;
using Application.Likes.Commands.UnlikePost;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/v1/posts/{slug}/likes")]
public class LikesController: BaseController
{
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [HttpPost]
  public async Task<IActionResult> LikePost(string slug)
  {
    var command = new LikePostCommand
    {
      Slug = slug
    };

    var response = await Mediator.Send(command);

    return Created(nameof(LikePost), response);
  }

  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [HttpDelete]
  public async Task<IActionResult> UnlikePost(string slug)
  {
    var command = new UnlikePostCommand
    {
      Slug = slug
    };

    var response = await Mediator.Send(command);

    return Ok(response);
  }
}
