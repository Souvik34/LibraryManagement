using System;
using LibraryManagement.Services;

namespace LibraryManagement.Services
{
    public static class AdminService
    {
        private const string adminUsername = "admin";
        private const string adminPassword = "admin123";

        public static void Login()
        {
            Console.Write("Enter Admin Username: ");
            string user = Console.ReadLine();
            Console.Write("Enter Admin Password: ");
            string pass = Console.ReadLine();

            if (user == adminUsername && pass == adminPassword)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Login successful!\n");
                Console.ResetColor();
                AdminMenu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid credentials.\n");
                Console.ResetColor();
            }
        }

        private static void AdminMenu()
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n========== Admin Menu ==========");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Update Book");
                Console.WriteLine("4. Delete Book");
                Console.WriteLine("5. Search Book");
                Console.WriteLine("6. View Borrowed Info");
                Console.WriteLine("7. Logout");
                Console.WriteLine("................................");
                Console.ResetColor();
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1": FileService.AddBook(); break;
                    case "2": FileService.ViewBooks(); break;
                    case "3": FileService.UpdateBook(); break;
                    case "4": FileService.DeleteBook(); break;
                    case "5": FileService.SearchBook(); break;
                    case "6": FileService.ViewBorrowedBooks(); break;
                    case "7":
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
