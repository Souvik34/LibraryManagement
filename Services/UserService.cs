using System;
using LibraryManagement.Services;

namespace LibraryManagement.Services
{
    public static class UserService
    {
        public static void Register()
        {
            Console.Write("Choose Username: ");
            string username = Console.ReadLine();
            Console.Write("Choose Password: ");
            string password = Console.ReadLine();

            FileService.RegisterUser(username, password);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Registration successful.");
            Console.ResetColor();
        }

        public static void Login()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            if (FileService.ValidateUser(username, password))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Login successful.\n");
                Console.ResetColor();
                UserMenu(username);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid credentials.\n");
                Console.ResetColor();
            }
        }

        private static void UserMenu(string username)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n========== User Menu ==========");
                Console.WriteLine("1. View Books");
                Console.WriteLine("2. Search Book");
                Console.WriteLine("3. Borrow Book");
                Console.WriteLine("4. Return Book");
                Console.WriteLine("5. Logout");
                Console.WriteLine("................................");
                Console.ResetColor();
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": FileService.ViewBooks(); break;
                    case "2": FileService.SearchBook(); break;
                    case "3": FileService.BorrowBook(username); break;
                    case "4": FileService.ReturnBook(username); break;
                    case "5":
                        loggedIn = false;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Logged out successfully.");
                        Console.ResetColor();
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
