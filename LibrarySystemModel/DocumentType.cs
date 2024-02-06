using System;
using System.Collections.Generic;

namespace LibrarySystemModel;

public partial class DocumentType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Reader> Readers { get; set; } = new List<Reader>();
}
