using System;
using LibraryManagement.Services;

namespace LibraryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Library Management System";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== Welcome to the Library Management System ==========\n");
            Console.ResetColor();

            FileService.InitializeFiles();

            bool exit = false;
            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. User Login");
                Console.WriteLine("3. Register");
                Console.WriteLine("4. Exit");
                Console.WriteLine("................................");
                Console.ResetColor();
                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": AdminService.Login(); break;
                    case "2": UserService.Login(); break;
                    case "3": UserService.Register(); break;
                    case "4":
                        exit = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Thank you for using the system. Goodbye!");
                        Console.ResetColor();
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
                Console.WriteLine("\n==============================================================\n");
            }
        }
    }
}
