using Microsoft.EntityFrameworkCore;
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

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }

    public void ViewAvailableBooks()
    {
        using (var context = new LibraryDatabaseContext())
        {
            Console.WriteLine("Available Books:");

            var availableBooks = context.Books
                .Include(b => b.Authors)
                .Where(b => b.Authors.Any())
                .OrderBy(b => b.ReturnDate)
                .ToList();

            foreach (var book in availableBooks)
            {
                Console.WriteLine($"{book.Id}. {book.Name} by {string.Join(", ", book.Authors.Select(a => $"{a.FirstName} {a.LastName}"))}");
            }
        }
    }

    public static void SearchBooksByAuthor(string authorName)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var authors = context.Authors
                .Where(a => (a.FirstName + " " + a.LastName).ToLower().Contains(authorName.ToLower()))
                .ToList();

            Console.WriteLine($"Authors with name '{authorName}':");
            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
            }
        }
    }

    public static void SearchBooksByTitle(string title)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var books = context.Books.Where(b => b.Name.ToLower().Contains(title.ToLower())).ToList();

            Console.WriteLine($"Books with title '{title}':");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}. {book.Name}");
            }
        }
    }

    public static void ViewAuthors()
    {
        using (var context = new LibraryDatabaseContext())
        {
            var authors = context.Authors.Include(a => a.Books).ToList();

            Console.WriteLine("Authors:");
            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}: {string.Join(", ", author.Books.Select(b => b.Name))}");
            }
        }
    }

    public void TakeBook(int readerId)
    {
        Console.WriteLine("Enter the ID of the book you want to borrow:");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            Console.WriteLine("Invalid book ID.");
            return;
        }

        using (var context = new LibraryDatabaseContext())
        {
            var book = context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            if (book.ReturnDate != null && book.ReturnDate < DateTime.Today)
            {
                Console.WriteLine("This book is overdue and cannot be borrowed.");
                return;
            }

            var borrowedBook = new BorrowedBook
            {
                BorrowDate = DateTime.Today,
                ReaderId = readerId,
                BookId = bookId,
                ReturnDate = DateTime.Today.AddDays(30)
            };

            context.BorrowedBooks.Add(borrowedBook);
            context.SaveChanges();

            Console.WriteLine("Book successfully borrowed.");
        }
    }

    public static void ViewTakenBooks(int readerId)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var reader = context.Readers
                .Include(r => r.BorrowedBooks)
                    .ThenInclude(bb => bb.Book)
                .FirstOrDefault(r => r.ReaderId == readerId);

            if (reader == null || reader.BorrowedBooks == null || !reader.BorrowedBooks.Any())
            {
                Console.WriteLine("No books currently taken by this reader.");
                return;
            }

            Console.WriteLine("Books taken by this reader:");

            var overdueBooks = reader.BorrowedBooks.Where(bb => bb.ReturnDate < DateTime.Today).OrderByDescending(bb => bb.ReturnDate);
            foreach (var borrowedBook in overdueBooks)
            {
                var book = borrowedBook.Book;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"- {book.Name} (Due Date: {borrowedBook.ReturnDate}) - Overdue");
                Console.ResetColor();
            }

            var otherBooks = reader.BorrowedBooks.Where(bb => bb.ReturnDate >= DateTime.Today).OrderBy(bb => bb.ReturnDate);
            foreach (var borrowedBook in otherBooks)
            {
                var book = borrowedBook.Book;
                Console.WriteLine($"- {book.Name} (Due Date: {borrowedBook.ReturnDate})");
            }
        }
    }

}

