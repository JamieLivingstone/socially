using System.Threading.Tasks;
using Application.Comments.CreateComment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
  [Route("api/v1/posts/{slug}/comments")]
  public class CommentsController : BaseController
  {
    [HttpPost]
    [ProducesResponseType(typeof(CreateCommentCommandDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateComment(string slug, CreateCommentCommand command)
    {
      if (slug != command.Slug)
      {
        return BadRequest();
      }

      var response = await Mediator.Send(command);

      return CreatedAtAction(nameof(CreateComment), response);
    }
  }
}
