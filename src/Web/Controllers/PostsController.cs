using System.Threading.Tasks;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Commands.UpdatePost;
using Application.Posts.Queries.GetPost;
using Application.Posts.Queries.GetPostList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/v1/posts")]
public class PostsController : BaseController
{
  [HttpGet("{slug}")]
  [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> GetPost(string slug)
  {
    var query = new GetPostQuery { Slug = slug };

    var response = await Mediator.Send(query);

    return Ok(response);
  }

  [HttpGet]
  public async Task<IActionResult> GetPosts(
    [FromQuery(Name = "pageNumber")] int pageNumber = 1,
    [FromQuery(Name = "pageSize")] int pageSize = 10,
    [FromQuery(Name = "orderBy")] PostListOrder orderBy = PostListOrder.Created,
    [FromQuery(Name = "author")] string author = "",
    [FromQuery(Name = "liked")] bool? liked = null,
    [FromQuery(Name = "tag")] string tag = "")
  {
    var query = new GetPostListQuery
    {
      PageNumber = pageNumber,
      PageSize = pageSize,
      OrderBy = orderBy,
      Author = author,
      Liked = liked ?? false,
      Tag = tag
    };

    var response = await Mediator.Send(query);

    return Ok(response);
  }

  [HttpPost]
  [ProducesResponseType(typeof(CreatePostDto), StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<IActionResult> CreatePost(CreatePostCommand command)
  {
    var response = await Mediator.Send(command);

    return CreatedAtAction(nameof(CreatePost), response);
  }

  [HttpPatch("{slug}")]
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
    var command = new DeletePostCommand
    {
      Slug = slug
    };

    var response = await Mediator.Send(command);

    return Ok(response);
  }
}
