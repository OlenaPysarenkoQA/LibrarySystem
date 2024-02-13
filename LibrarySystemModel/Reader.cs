using System;
using System.Collections.Generic;

namespace LibrarySystemModel;

public partial class Reader
{
    public int ReaderId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? DocumentTypeId { get; set; }

    public string? NumDoc { get; set; }

    public virtual DocumentType? DocumentType { get; set; }

    public virtual ICollection<Librarian> Librarians { get; set; } = new List<Librarian>();
}
