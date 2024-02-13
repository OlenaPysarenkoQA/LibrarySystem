using System;
using System.Collections.Generic;

namespace LibrarySystemModel;

public partial class Book
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? PublisherCode { get; set; }

    public int? PublishingTypeId { get; set; }

    public int? Year { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public virtual PublishingType? PublishingType { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
