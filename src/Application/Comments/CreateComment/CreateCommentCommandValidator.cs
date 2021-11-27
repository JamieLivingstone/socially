using FluentValidation;

namespace Application.Comments.CreateComment
{
  public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
  {
    public CreateCommentCommandValidator()
    {
      RuleFor(x => x.Message)
        .NotNull()
        .NotEmpty()
        .MinimumLength(5);

      RuleFor(x => x.Slug)
        .NotNull()
        .NotEmpty();
    }
  }
}
