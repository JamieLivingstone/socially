using System;
using System.Collections.Generic;
using Domain.Entities;
using Infrastructure.Persistence;

namespace IntegrationTests.Application.TestUtils
{
  public class Seed
  {
    public const int CurrentUserId = 1;

    public static void InitializeDbForTests(ApplicationDbContext dbContext)
    {
      dbContext.Persons.AddRange(Persons());
      dbContext.Posts.AddRange(Posts());
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
        },
        new()
        {
          Id = 2,
          Name = "Peter Gray",
          Username = "peter",
          Email = "peter.gray@test.com",
        }
      };
    }

    public static IEnumerable<Post> Posts()
    {
      return new List<Post>
      {
        new()
        {
          Id = 1,
          Slug = "test-post-one",
          Title = "Test Post One",
          Body = "Test Post One Body",
          CreatedAt = new DateTime(2021, 01, 01),
          UpdatedAt = new DateTime(2021, 01, 01),
          AuthorId = CurrentUserId,
        },
        new()
        {
          Id = 2,
          Slug = "test-post-two",
          Title = "Test Post Two",
          Body = "Test Post Two Body",
          CreatedAt = new DateTime(2020, 05, 04),
          UpdatedAt = new DateTime(2020, 05, 04),
          AuthorId = 2,
        }
      };
    }
  }
}
