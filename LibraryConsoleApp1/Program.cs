using LibrarySystemModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.PortableExecutable;

namespace LibraryConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Library System!");

            while (true)
            {
                Console.WriteLine("Select action (login-l/register-r/exit-ex):");
                string action = Console.ReadLine();

                if (action.ToLower() == "l")
                {
                    Login();
                                    }
                else if (action.ToLower() == "r")
                {
                    Register();
                }
                else if (action.ToLower() == "ex")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid action. Please try again.");
                }
            }
        }

        static void LibrarianMenu()
        {
            Librarian librarian = new Librarian();

            Console.WriteLine("Librarian Menu:");
            Console.WriteLine("1. View all books and authors");
            Console.WriteLine("2. Add/update books and authors");
            Console.WriteLine("3. Add/update/delete readers");
            Console.WriteLine("4. View reader information");
            Console.WriteLine("5. Exit");

            while (true)
            {
                Console.WriteLine("Select option:");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Viewing all books and authors...");
                        Librarian.ViewAllBooksAndAuthors();
                        break;
                    case "2":
                        Console.WriteLine("Adding/updating books and authors...");
                        Librarian.AddOrUpdateBook();
                        break;
                    case "3":
                        Console.WriteLine("Adding/updating/deleting readers...");
                        while (true)
                        {
                            Console.WriteLine("Select action (add-a/update-u/delete-d/exit-ex):");
                            string actionReader = Console.ReadLine();

                            if (actionReader.ToLower() == "a")
                            {
                                Console.WriteLine("Enter login:");
                                string login = Console.ReadLine();
                                Console.WriteLine("Enter password:");
                                string password = Console.ReadLine();
                                Console.WriteLine("Enter email:");
                                string email = Console.ReadLine();
                                Console.WriteLine("Enter first name:");
                                string firstName = Console.ReadLine();
                                Console.WriteLine("Enter last name:");
                                string lastName = Console.ReadLine();
                                Console.WriteLine("Enter document type ID:");
                                int documentTypeId = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter document number:");
                                string numDoc = Console.ReadLine();

                                bool success = librarian.AddReader(login, password, email, firstName, lastName, documentTypeId, numDoc);
                                if (success)
                                {
                                    Console.WriteLine("Reader successfully added.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to add reader. A reader with the same login already exists.");
                                }
                            }
                            else if (actionReader.ToLower() == "u")
                            {
                                Console.WriteLine("Enter reader ID:");
                                if (!int.TryParse(Console.ReadLine(), out int readerId))
                                {
                                    Console.WriteLine("Invalid reader ID.");
                                    break;
                                }
                                Console.WriteLine("Enter email:");
                                string email = Console.ReadLine();
                                Console.WriteLine("Enter first name:");
                                string firstName = Console.ReadLine();
                                Console.WriteLine("Enter last name:");
                                string lastName = Console.ReadLine();
                                Console.WriteLine("Enter document type ID:");
                                int documentTypeId = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter document number:");
                                string numDoc = Console.ReadLine();

                                bool success = librarian.UpdateReader(readerId, email, firstName, lastName, documentTypeId, numDoc);
                                if (success)
                                {
                                    Console.WriteLine("Reader information successfully updated.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to update reader information. Reader not found.");
                                }
                            }
                            else if (actionReader.ToLower() == "d")
                            {
                                Console.WriteLine("Enter reader ID:");
                                if (!int.TryParse(Console.ReadLine(), out int readerId))
                                {
                                    Console.WriteLine("Invalid reader ID.");
                                    break;
                                }

                                bool success = librarian.DeleteReader(readerId);
                                if (success)
                                {
                                    Console.WriteLine("Reader successfully deleted.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to delete reader. Reader not found.");
                                }
                            }
                            else if (actionReader.ToLower() == "ex")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid action. Please try again.");
                            }
                        }
                        break;
                    case "4":
                        Console.WriteLine("Viewing reader information. Select sub-action:");
                        Console.WriteLine("1. View overdue borrowers and their obligations");
                        Console.WriteLine("2. View all readers who borrowed books and their borrowed books");
                        Console.WriteLine("3. View borrowing and returning history for a specific reader (including overdue counts)");
                        string subAction = Console.ReadLine();
                        switch (subAction)
                        {
                            case "1":
                                Librarian.ViewDebtors();
                                break;
                            case "2":
                                Librarian.ViewReadersInfo();
                                break;
                            case "3":
                                Console.WriteLine("Enter reader ID:");
                                if (!int.TryParse(Console.ReadLine(), out int readerId))
                                {
                                    Console.WriteLine("Invalid reader ID.");
                                    break;
                                }
                                librarian.ViewReaderHistory(readerId);
                                break;
                            default:
                                Console.WriteLine("Invalid sub-action. Please try again.");
                                break;
                        }
                        break;
                    case "5":
                        return; 
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void ReaderMenu()
        {
            Console.WriteLine("Reader Menu:");
            Console.WriteLine("1. Search books by author");
            Console.WriteLine("2. Search books by title");
            Console.WriteLine("3. View information about authors");
            Console.WriteLine("4. View taken books");
            Console.WriteLine("5. Take a book");
            Console.WriteLine("6. Exit");

            while (true)
            {
                Console.WriteLine("Select option:");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("Searching books by author...");
                        Reader.SearchBooksByAuthor("Іван");
                        break;
                    case "2":
                        Console.WriteLine("Searching books by title...");
                        Reader.SearchBooksByTitle("Дім в якому");
                        break;
                    case "3":
                        Console.WriteLine("Viewing information about authors...");
                        Reader.ViewAuthors();
                        break;
                    case "4":
                        Console.WriteLine("Enter reader ID:");
                        if (!int.TryParse(Console.ReadLine(), out int readerId))
                        {
                            Console.WriteLine("Invalid reader ID.");
                            break;
                        }
                        Console.WriteLine("Viewing taken books...");
                        Reader.ViewTakenBooks(readerId);
                        break;
                    case "5":
                        Console.WriteLine("Enter your reader ID:");
                        if (!int.TryParse(Console.ReadLine(), out int chosenReaderId))
                        {
                            Console.WriteLine("Invalid reader ID.");
                            break;
                        }
                        Console.WriteLine("Taking a book...");
                        Reader reader = new Reader();
                        reader.TakeBook(chosenReaderId);
                        break;
                    case "6":
                        return; 
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void Login()
        {
            Console.WriteLine("Login:");
            string login = Console.ReadLine();
            Console.WriteLine("Password:");
            string password = Console.ReadLine();

            using (var context = new LibraryDatabaseContext())
            {
                var librarian = context.Librarians.FirstOrDefault(l => l.Login == login && l.Password == password);
                if (librarian != null)
                {
                    Console.WriteLine($"Welcome, Librarian {login}!");
                    LibrarianMenu();
                    return;
                }

                var reader = context.Readers.FirstOrDefault(r => r.Login == login && r.Password == password);
                if (reader != null)
                {
                    Console.WriteLine($"Welcome, Reader {login}!");
                    ReaderMenu();
                    return;
                }

                Console.WriteLine("Login failed. Please try again.");
            }
        }

        static void Register()
        {
            Console.WriteLine("Select user type to register (librarian-l/reader-r):");
            string userType = Console.ReadLine();

            Console.WriteLine("Enter login:");
            string login = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            using (var context = new LibraryDatabaseContext())
            {
                if (userType.ToLower() == "l")
                {
                    var librarian = new Librarian { Login = login, Password = password, Email = email };
                    context.Librarians.Add(librarian);
                }
                else if (userType.ToLower() == "r")
                {
                    Console.WriteLine("Enter first name:");
                    string firstName = Console.ReadLine();
                    Console.WriteLine("Enter last name:");
                    string lastName = Console.ReadLine();
                    Console.WriteLine("Enter document type ID:");
                    int documentTypeId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter document number:");
                    string numDoc = Console.ReadLine();

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
                }

                context.SaveChanges();

                Console.WriteLine($"Successfully registered as {userType}.");
            }
        }

        
        
    }
}