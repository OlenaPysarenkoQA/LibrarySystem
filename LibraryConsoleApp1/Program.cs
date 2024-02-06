using LibrarySystemModel;
using System;

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
                    return;
                }

                var reader = context.Readers.FirstOrDefault(r => r.Login == login && r.Password == password);
                if (reader != null)
                {
                    Console.WriteLine($"Welcome, Reader {login}!");
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