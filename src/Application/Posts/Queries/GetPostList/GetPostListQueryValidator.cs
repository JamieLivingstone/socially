using FluentValidation;

namespace Application.Posts.Queries.GetPostList;

public class GetPostListQueryValidator : AbstractValidator<GetPostListQuery>
{
  public GetPostListQueryValidator()
  {
    RuleFor(x => x.PageNumber)
      .GreaterThanOrEqualTo(1);

    RuleFor(x => x.PageSize)
      .GreaterThanOrEqualTo(1)
      .LessThanOrEqualTo(50);

    RuleFor(x => x.OrderBy)
      .IsInEnum();
  }
}
