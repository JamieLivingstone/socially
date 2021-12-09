using FluentValidation;

namespace Application.Comments.Queries.GetCommentsWithPagination;

public class GetCommentsWithPaginationQueryValidator : AbstractValidator<GetCommentsWithPaginationQuery>
{
  public GetCommentsWithPaginationQueryValidator()
  {
    RuleFor(x => x.Slug)
      .NotNull()
      .NotEmpty();

    RuleFor(x => x.PageNumber)
      .GreaterThanOrEqualTo(1);

    RuleFor(x => x.PageSize)
      .GreaterThanOrEqualTo(1)
      .LessThanOrEqualTo(100);
  }
}
