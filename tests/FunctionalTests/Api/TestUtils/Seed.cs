using System;
using System.Collections.Generic;
using Domain.Entities;
using Infrastructure.Persistence;

namespace FunctionalTests.Api.TestUtils
{
  public static class Seed
  {
    public const int CurrentUserId = 1;

    public static void InitializeDbForTests(ApplicationDbContext dbContext)
    {
      dbContext.Persons.AddRange(Persons());
      dbContext.Posts.AddRange(Posts());
      dbContext.Comments.AddRange(Comments());
      dbContext.SaveChanges();
    }

    public static IEnumerable<Person> Persons()
    {
      return new List<Person>
      {
        new()
        {
          Id = CurrentUserId,
          Name = "Liam Hale",
          Username = "liam",
          Email = "liam.hale@test.com",
          Hash = new byte[64],
          Salt = new byte[64]
        },
        new()
        {
          Id = 2,
          Name = "Peter Gray",
          Username = "peter",
          Email = "peter.gray@test.com",
          Hash = new byte[64],
          Salt = new byte[64]
        }
      };
    }

    public static IEnumerable<Post> Posts()
    {
      return new List<Post>
      {
        new()
        {
          Id = CurrentUserId,
          Slug = "test-post-one",
          Title = "Test Post One",
          Body = "Test Post One Body",
          CreatedAt = new DateTime(2021, 01, 01),
          UpdatedAt = new DateTime(2021, 01, 01),
          AuthorId = CurrentUserId
        },
        new()
        {
          Id = 2,
          Slug = "test-post-two",
          Title = "Test Post Two",
          Body = "Test Post Two Body",
          CreatedAt = new DateTime(2020, 05, 04),
          UpdatedAt = new DateTime(2020, 05, 04),
          AuthorId = 2
        }
      };
    }

    public static IEnumerable<Comment> Comments()
    {
      return new List<Comment>
      {
        new()
        {
          Id = 1,
          Message = "Mock comment one",
          PostId = 1,
          AuthorId = CurrentUserId
        },
        new()
        {
          Id = 2,
          Message = "Mock comment two",
          PostId = 1,
          AuthorId = 2
        }
      };
    }
  }
}
