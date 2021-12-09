using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Posts.Commands.CreatePost;
using Domain.Entities;
using FluentAssertions;
using IntegrationTests.Application.TestUtils;
using NUnit.Framework;
using Snapshooter.NUnit;

namespace IntegrationTests.Application.Posts;

[TestFixture]
public class CreatePostTests : TestBase
{
  [Test]
  public void GivenAnInvalidRequest_ThrowsValidationException()
  {
    var command = new CreatePostCommand
    {
      Title = "", // Required field
      Body = "Body!"
    };

    FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
  }

  [Test]
  public async Task GivenAValidRequest_CreatesPost()
  {
    var command = new CreatePostCommand
    {
      Title = "Test post title",
      Body = "Test post body"
    };

    var result = await SendAsync(command);
    var post = await FindAsync<Post>(p => p.Slug == result.Slug);

    Snapshot.Match(post, options =>
    {
      options.IgnoreField("Id");
      options.IgnoreField("CreatedAt");
      options.IgnoreField("UpdatedAt");

      return options;
    });
  }

  [Test]
  public async Task GivenDuplicateTitle_GeneratesUniqueSlug()
  {
    var command = new CreatePostCommand
    {
      Title = "Test post title",
      Body = "Test post body"
    };

    var resultOne = await SendAsync(command);
    var resultTwo = await SendAsync(command);

    Assert.AreEqual("test-post-title", resultOne.Slug);
    Assert.That(resultTwo.Slug, Does.Match("test-post-title-[a-zA-Z0-9_.-]{11}$"));
  }
}
