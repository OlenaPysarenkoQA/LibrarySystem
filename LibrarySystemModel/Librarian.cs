using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace LibrarySystemModel;

public partial class Librarian
{
    public int LibrarianId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Reader> Readers { get; set; } = new List<Reader>();

    public bool AddReader(string login, string password, string email, string firstName, string lastName, int documentTypeId, string numDoc)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var existingReader = context.Readers.FirstOrDefault(r => r.Login == login);
            if (existingReader != null)
                return false; 

            var newReader = new Reader()
            {
                Login = login,
                Password = password,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                DocumentTypeId = documentTypeId,
                NumDoc = numDoc
            };

            context.Readers.Add(newReader);
            context.SaveChanges();

            return true;
        }
    }

    public bool UpdateReader(int readerId, string email, string firstName, string lastName, int documentTypeId, string numDoc)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var reader = context.Readers.FirstOrDefault(r => r.ReaderId == readerId);
            if (reader == null)
                return false; 

            reader.Email = email;
            reader.FirstName = firstName;
            reader.LastName = lastName;
            reader.DocumentTypeId = documentTypeId;
            reader.NumDoc = numDoc;

            context.SaveChanges();

            return true;
        }
    }

    public bool DeleteReader(int readerId)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var reader = context.Readers.FirstOrDefault(r => r.ReaderId == readerId);
            if (reader == null)
                return false; 

            context.Readers.Remove(reader);
            context.SaveChanges();

            return true;
        }
    }

    public static void AddOrUpdateBook()
    {
        Console.WriteLine("Enter the ID of the book to update (or 0 to add new book):");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            Console.WriteLine("Invalid book ID.");
            return;
        }

        using (var context = new LibraryDatabaseContext())
        {
            Book book = null;
            if (bookId != 0)
            {
                book = context.Books.Include(b => b.Authors).FirstOrDefault(b => b.Id == bookId);
                if (book == null)
                {
                    Console.WriteLine("Book not found.");
                    return;
                }

                Console.WriteLine("Enter the new name of the book:");
                book.Name = Console.ReadLine();
                
                book.Authors.Clear();
            }
            else
            {
                book = new Book();
                Console.WriteLine("Enter the name of the new book:");
                book.Name = Console.ReadLine();
            }

            Console.WriteLine("Enter the number of authors for this book:");
            if (!int.TryParse(Console.ReadLine(), out int authorCount))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            for (int i = 0; i < authorCount; i++)
            {
                Console.WriteLine($"Enter the name of author #{i + 1}:");
                var authorName = Console.ReadLine();
                var author = context.Authors.FirstOrDefault(a => (a.FirstName + " " + a.LastName).ToLower() == authorName.ToLower());
                if (author == null)
                {
                    author = new Author();
                    var names = authorName.Split(' ');
                    author.FirstName = names[0];
                    author.LastName = names[1];
                    context.Authors.Add(author);
                }
                book.Authors.Add(author);
            }

            if (bookId == 0)
            {
                context.Books.Add(book);
            }

            context.SaveChanges();

            Console.WriteLine("Book information updated successfully.");
        }
    }

    public static void ViewAllBooksAndAuthors()
    {
        using (var context = new LibraryDatabaseContext())
        {
            var books = context.Books.Include(b => b.Authors).ToList();

            Console.WriteLine("All Books and Authors:");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Name} by {string.Join(", ", book.Authors.Select(a => $"{a.FirstName} {a.LastName}"))}");
            }
        }
    }

    public void AddNewBook(string name, List<string> authorNames)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var authors = new List<Author>();

            foreach (var authorName in authorNames)
            {
                var splitNames = authorName.Split(' ');
                var existingAuthor = context.Authors.FirstOrDefault(a => a.FirstName.ToLower() == splitNames[0].ToLower() && a.LastName.ToLower() == splitNames[1].ToLower());

                if (existingAuthor != null)
                {
                    authors.Add(existingAuthor);
                }
                else
                {
                    var newAuthor = new Author { FirstName = splitNames[0], LastName = splitNames[1] };
                    context.Authors.Add(newAuthor);
                    authors.Add(newAuthor);
                }
            }

            foreach (var author in authors)
            {
                context.Entry(author).State = EntityState.Detached;
            }

            var newBook = new Book { Name = name, Authors = authors };
            context.Books.Add(newBook);
            context.SaveChanges();

            Console.WriteLine("New book added successfully.");
        }
    }

    public void UpdateBook(int bookId, string newName, List<string> newAuthorNames)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var book = context.Books.Include(b => b.Authors).FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            book.Name = newName;

            book.Authors.Clear();
            foreach (var authorName in newAuthorNames)
            {
                var existingAuthor = context.Authors.FirstOrDefault(a => (a.FirstName + " " + a.LastName).ToLower() == authorName.ToLower());
                if (existingAuthor != null)
                {
                    book.Authors.Add(existingAuthor);
                }
                else
                {
                    var newAuthor = new Author { FirstName = authorName.Split(' ')[0], LastName = authorName.Split(' ')[1] };
                    context.Authors.Add(newAuthor);
                    book.Authors.Add(newAuthor);
                }
            }

            context.SaveChanges();

            Console.WriteLine("Book information updated successfully.");
        }
    }

    public void DeleteBook(int bookId)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var book = context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            context.Books.Remove(book);
            context.SaveChanges();

            Console.WriteLine("Book deleted successfully.");
        }
    }

    public static void ViewReadersInfo()
    {
        using (var context = new LibraryDatabaseContext())
        {
            var debtors = context.Readers
                .Where(r => r.BorrowedBooks.Any(b => b.ReturnDate < DateTime.Today))
                .ToList();

            Console.WriteLine("Debtors:");
            foreach (var debtor in debtors)
            {
                if (debtor.FirstName != null && debtor.LastName != null)
                {
                    Console.WriteLine($"{debtor.FirstName} {debtor.LastName} (Reader ID: {debtor.ReaderId}) is a debtor.");

                    var overdueBooks = debtor.BorrowedBooks.Where(b => b.ReturnDate < DateTime.Today).ToList();
                    Console.WriteLine("Overdue Books:");
                    foreach (var book in overdueBooks)
                    {
                        Console.WriteLine($"- {book.Book.Name} (Due Date: {book.ReturnDate})");
                    }
                }
                else
                {
                    Console.WriteLine("Debtor's first name or last name is null.");
                }
            }

            var allReaders = context.Readers.Include(r => r.BorrowedBooks).ToList();

            Console.WriteLine("All Readers and their Books:");
            foreach (var reader in allReaders)
            {
                Console.WriteLine($"{reader.FirstName} {reader.LastName} (Reader ID: {reader.ReaderId}):");
                foreach (var book in reader.BorrowedBooks)
                {
                    if (book.ReturnDate < DateTime.Today)
                    {
                        Console.WriteLine($"- {book.Book.Name} (Due Date: {book.ReturnDate}) - Overdue");
                    }
                    else
                    {
                        Console.WriteLine($"- {book.Book.Name} (Due Date: {book.ReturnDate})");
                    }
                }
            }
        }
    }

    public void ViewReaderHistory(int readerId)
    {
        using (var context = new LibraryDatabaseContext())
        {
            var reader = context.Readers.FirstOrDefault(r => r.ReaderId == readerId);
            if (reader == null)
            {
                Console.WriteLine("Reader not found.");
                return;
            }

            Console.WriteLine($"Reader: {reader.FirstName} {reader.LastName} (Reader ID: {reader.ReaderId})");
            Console.WriteLine("Books History:");

            var booksHistory = context.BorrowedBooks
                .Where(b => b.ReaderId == readerId)
                .Select(b => new { BookName = b.Book.Name, TakenDate = b.BorrowDate, ReturnDate = b.ReturnDate })
                .ToList();

            foreach (var book in booksHistory)
            {
                if (book.ReturnDate.HasValue)
                {
                    if (book.ReturnDate < DateTime.Today)
                    {
                        Console.WriteLine($"- {book.BookName} (Taken Date: {book.TakenDate}, Returned Date: {book.ReturnDate}) - Overdue");
                    }
                    else
                    {
                        Console.WriteLine($"- {book.BookName} (Taken Date: {book.TakenDate}, Returned Date: {book.ReturnDate})");
                    }
                }
                else
                {
                    Console.WriteLine($"- {book.BookName} (Taken Date: {book.TakenDate}) - Not returned yet");
                }
            }
        }
    }

    public static void ViewDebtors()
    {
        using (var context = new LibraryDatabaseContext())
        {
            var debtors = context.Readers
                .Include(r => r.BorrowedBooks)
                .Where(r => r.BorrowedBooks.Any(bb => bb.ReturnDate < DateTime.Today))
                .ToList();

            Console.WriteLine("Debtors:");
            foreach (var debtor in debtors)
            {
                Console.WriteLine($"{debtor.FirstName} {debtor.LastName} (Reader ID: {debtor.ReaderId}) is a debtor.");

                var overdueBooks = debtor.BorrowedBooks.Where(bb => bb.ReturnDate < DateTime.Today).ToList();
                Console.WriteLine("Overdue Books:");
                foreach (var book in overdueBooks)
                {
                    Console.WriteLine($"- {book.Book.Name} (Due Date: {book.ReturnDate})");
                }
            }
        }
    }

    public void ViewAllReadersAndBooks()
    {
        using (var context = new LibraryDatabaseContext())
        {
            var allReaders = context.Readers
                .Include(r => r.BorrowedBooks)
                .ThenInclude(bb => bb.Book)
                .ToList();

            Console.WriteLine("All Readers and their Books:");
            foreach (var reader in allReaders)
            {
                Console.WriteLine($"{reader.FirstName} {reader.LastName} (Reader ID: {reader.ReaderId}):");
                foreach (var borrowedBook in reader.BorrowedBooks)
                {
                    if (borrowedBook.ReturnDate < DateTime.Today)
                    {
                        Console.WriteLine($"- {borrowedBook.Book.Name} (Due Date: {borrowedBook.ReturnDate}) - Overdue");
                    }
                    else
                    {
                        Console.WriteLine($"- {borrowedBook.Book.Name} (Due Date: {borrowedBook.ReturnDate})");
                    }
                }
            }
        }
    }

}
