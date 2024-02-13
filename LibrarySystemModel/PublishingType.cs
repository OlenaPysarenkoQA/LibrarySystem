using System;
using System.Collections.Generic;

namespace LibrarySystemModel;

public partial class PublishingType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
