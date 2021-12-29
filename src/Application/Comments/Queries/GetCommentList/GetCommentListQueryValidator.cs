using FluentValidation;

namespace Application.Comments.Queries.GetCommentList;

public class GetCommentListQueryValidator : AbstractValidator<GetCommentListQuery>
{
  public GetCommentListQueryValidator()
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
