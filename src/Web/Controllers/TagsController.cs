using System.Threading.Tasks;
using Application.Common.Models;
using Application.Tags.Queries.GetTagList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;


[Route("api/v1/tags")]
public class TagsController : BaseController
{
  [HttpGet]
  [ProducesResponseType(typeof(PaginatedList<TagListDto>), StatusCodes.Status200OK)]
  public async Task<IActionResult> GetPosts(
    [FromQuery(Name = "term")] string term = "",
    [FromQuery(Name = "pageNumber")] int pageNumber = 1,
    [FromQuery(Name = "pageSize")] int pageSize = 10,
    [FromQuery(Name = "orderBy")] TagListOrder orderBy = TagListOrder.Popularity
    ){
    var query = new GetTagListQuery
    {
      Term = term,
      PageNumber = pageNumber,
      PageSize = pageSize,
      OrderBy = orderBy
    };

    var response = await Mediator.Send(query);

    return Ok(response);
  }
}
