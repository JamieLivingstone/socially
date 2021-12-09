using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
  public void Configure(EntityTypeBuilder<Person> builder)
  {
    builder.Property(p => p.Name)
      .IsRequired();

    builder.Property(p => p.Username)
      .IsRequired();

    builder.Property(p => p.Email)
      .IsRequired();

    builder.Property(p => p.Hash)
      .IsRequired();

    builder.Property(p => p.Salt)
      .IsRequired();

    builder.HasIndex(p => p.Username)
      .IsUnique();

    builder.HasIndex(p => p.Email)
      .IsUnique();
  }
}
