﻿using System;
using System.Collections.Generic;

namespace LibrarySystemModel;

public partial class Author
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? SecondName { get; set; }

    public DateTime? BirthDate { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
