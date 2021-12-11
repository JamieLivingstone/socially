using System.Collections.Generic;

namespace Domain.Entities;

public class Tag
{
  public string TagId { get; init; }

  public IEnumerable<PostTag> PostTags { get; set; }
}
