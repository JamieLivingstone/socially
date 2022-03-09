using System.Threading.Tasks;
using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Comments.Queries.GetCommentList;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/v1/posts/{slug}/comments")]
public class CommentsController : BaseController
{
  [HttpGet]
  [ProducesResponseType(typeof(PaginatedList<CommentListDto>), StatusCodes.Status200OK)]
  public async Task<IActionResult> GetComments(string slug = "",
    [FromQuery(Name = "pageNumber")] int pageNumber = 1,
    [FromQuery(Name = "pageSize")] int pageSize = 10)
  {
    var query = new GetCommentListQuery
    {
      Slug = slug,
      PageNumber = pageNumber,
      PageSize = pageSize
    };

    var response = await Mediator.Send(query);

    return Ok(response);
  }

  [HttpPost]
  [ProducesResponseType(typeof(CreateCommentCommandDto), StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> CreateComment(string slug, CreateCommentCommand command)
  {
    if (slug != command.Slug)
    {
      return BadRequest();
    }

    var response = await Mediator.Send(command);

    return CreatedAtAction(nameof(CreateComment), response);
  }

  [HttpDelete("{commentId:int}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteComment(string slug, int commentId)
  {
    var command = new DeleteCommentCommand
    {
      CommentId = commentId
    };

    var response = await Mediator.Send(command);

    return Ok(response);
  }
}
