using System.Threading.Tasks;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.LikePost;
using Application.Posts.Commands.UpdatePost;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
  [Route("api/v1/posts")]
  public class PostsController : BaseController
  {
    [HttpPost]
    [Produces("text/plain", "application/json")]
    [ProducesResponseType(typeof(CreatePostDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreatePost(CreatePostCommand command)
    {
      var response = await Mediator.Send(command);

      return CreatedAtAction(nameof(CreatePost), response);
    }

    [HttpPatch("{slug}")]
    [Produces("text/plain", "application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdatePost(string slug, UpdatePostCommand command)
    {
      if (slug != command.Slug)
      {
        return BadRequest();
      }

      var response = await Mediator.Send(command);

      return Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpDelete("{slug}")]
    public async Task<IActionResult> DeletePost(string slug)
    {
      var command = new DeletePostCommand { Slug = slug };

      var response = await Mediator.Send(command);

      return Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("{slug}/likes")]
    public async Task<IActionResult> LikePost(string slug)
    {
      var command = new LikePostCommand { Slug = slug };

      var response = await Mediator.Send(command);

      return Created(nameof(LikePost), response);
    }
  }
}
