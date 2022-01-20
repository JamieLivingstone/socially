using FluentValidation;

namespace Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
  public CreatePostCommandValidator()
  {
    RuleFor(x => x.Title)
      .NotNull()
      .NotEmpty()
      .MaximumLength(150);

    RuleFor(x => x.Body)
      .NotNull()
      .NotEmpty()
      .MaximumLength(10000);

    RuleForEach(x => x.Tags)
      .NotNull()
      .NotEmpty()
      .Matches("^[a-zA-Z]*$").WithMessage("{PropertyName} must only contain letters")
      .MaximumLength(20);
  }
}
