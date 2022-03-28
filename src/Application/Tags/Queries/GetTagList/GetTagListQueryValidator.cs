using FluentValidation;

namespace Application.Tags.Queries.GetTagList;

public class GetTagListQueryValidator : AbstractValidator<GetTagListQuery>
{
  public GetTagListQueryValidator()
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
