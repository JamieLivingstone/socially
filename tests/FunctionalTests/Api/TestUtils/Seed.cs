using System;
using System.Collections.Generic;
using Domain.Entities;
using Infrastructure.Persistence;

namespace FunctionalTests.Api.TestUtils
{
  public class Seed
  {
    public static void InitializeDbForTests(ApplicationDbContext dbContext)
    {
      dbContext.Persons.AddRange(Persons());
      dbContext.SaveChanges();
    }

    public static IEnumerable<Person> Persons()
    {
      return new List<Person>
      {
        new()
        {
          Id = 1,
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
  }
}
