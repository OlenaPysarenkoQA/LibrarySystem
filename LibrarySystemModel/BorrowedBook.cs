using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystemModel
{
    public partial class BorrowedBook
    {
        public int Id { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int ReaderId { get; set; }

        public Reader Reader { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

    }
}
