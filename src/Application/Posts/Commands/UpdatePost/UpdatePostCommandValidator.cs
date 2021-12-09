using FluentValidation;

namespace Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
  public UpdatePostCommandValidator()
  {
    RuleFor(x => x.Title)
      .NotEmpty()
      .MaximumLength(150)
      .When(x => x.Title != null);

    RuleFor(x => x.Body)
      .NotEmpty()
      .MaximumLength(10000)
      .When(x => x.Body != null);
    ;
  }
}
