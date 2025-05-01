using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public static class FileService
    {
        private static string booksFile = "books.csv";
        private static string usersFile = "users.csv";
        private static string borrowedFile = "borrowed.csv";

        public static void InitializeFiles()
        {
            if (!File.Exists(booksFile)) File.Create(booksFile).Close();
            if (!File.Exists(usersFile)) File.Create(usersFile).Close();
            if (!File.Exists(borrowedFile)) File.Create(borrowedFile).Close();
        }

        // BOOK CRUD METHODS
        public static void AddBook()
        {
            Console.Write("Enter Book ID: ");
            string id = Console.ReadLine();  
            Console.Write("Enter Book Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Author: ");
            string author = Console.ReadLine();
            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity)) quantity = 0;

            // Append book information to the CSV file
            File.AppendAllText(booksFile, $"{id},{title},{author},{quantity}\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book added successfully.");
            Console.ResetColor();
        }


        public static void ViewBooks()
        {
            var books = ReadBooks();
            if (!books.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No books found.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== Book List ==========");
            foreach (var book in books)
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Qty: {book.Quantity}");
            Console.ResetColor();
        }

        public static void UpdateBook()
        {
            ViewBooks();
            Console.Write("Enter Book ID to update: ");
            string id = Console.ReadLine();
            var books = ReadBooks();
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.Write("New Title: ");
            book.Title = Console.ReadLine();
            Console.Write("New Author: ");
            book.Author = Console.ReadLine();
            Console.Write("New Quantity: ");
            book.Quantity = int.Parse(Console.ReadLine());

            WriteBooks(books);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book updated.");
            Console.ResetColor();
        }

        public static void DeleteBook()
        {
            ViewBooks();
            Console.Write("Enter Book ID to delete: ");
            string id = Console.ReadLine();
            var books = ReadBooks().Where(b => b.Id != id).ToList();
            WriteBooks(books);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book deleted.");
            Console.ResetColor();
        }

        public static void SearchBook()
        {
            Console.Write("Enter title or author to search: ");
            string query = Console.ReadLine().ToLower();
            var results = ReadBooks().Where(b =>
                b.Title.ToLower().Contains(query) || b.Author.ToLower().Contains(query)).ToList();

            if (!results.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No matching books found.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== Search Results ==========");
            foreach (var book in results)
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Qty: {book.Quantity}");
            Console.ResetColor();
        }

        public static void ViewBorrowedBooks()
        {
            var borrowedBooks = File.ReadAllLines(borrowedFile)
                                    .Where(line => !string.IsNullOrWhiteSpace(line)) // Skip empty lines
                                    .Select(line =>
                                    {
                                        var parts = line.Split(',');

                                        // Check if the line contains the correct number of columns
                                        if (parts.Length == 3)
                                        {
                                            return new Borrowed
                                            {
                                                Username = parts[0],
                                                BookId = parts[1],
                                                Title = parts[2]
                                            };
                                        }
                                        return null;
                                    })
                                    .Where(borrowedBook => borrowedBook != null)
                                    .ToList();

            if (borrowedBooks.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No books have been borrowed yet.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n========== Borrowed Books ==========");
                foreach (var borrowedBook in borrowedBooks)
                {
                    Console.WriteLine($"Username: {borrowedBook.Username}, Book ID: {borrowedBook.BookId}, Title: {borrowedBook.Title}");
                }
                Console.ResetColor();
            }
        }


        // CSV Parsers
        public static List<Book> ReadBooks()
        {
            return File.ReadAllLines(booksFile)
                       .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("Id")) // Skip header
                       .Select(line =>
                       {
                           var parts = line.Split(',');
                           return new Book
                           {
                               Id = parts[0],
                               Title = parts[1],
                               Author = parts[2],
                               Quantity = int.TryParse(parts[3], out int quantity) ? quantity : 0 // Handle invalid quantity
                           };
                       }).ToList();
        }


        public static void WriteBooks(List<Book> books)
        {
            File.WriteAllLines(booksFile, books.Select(b => $"{b.Id},{b.Title},{b.Author},{b.Quantity}"));
        }

        public static void RegisterUser(string username, string password)
        {
            File.AppendAllText(usersFile, $"{username},{password}\n");
        }

        public static bool ValidateUser(string username, string password)
        {
            return File.ReadLines(usersFile).Any(u => u == $"{username},{password}");
        }

        public static bool HasUserBorrowed(string username, string bookId)
        {
            return File.ReadLines(borrowedFile).Any(b => b.StartsWith($"{username},{bookId}"));
        }

        public static void BorrowBook(string username)
        {
            var books = ReadBooks();
            ViewBooks();
            Console.Write("Enter Book ID to borrow: ");
            string id = Console.ReadLine();
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null || book.Quantity <= 0)
            {
                Console.WriteLine("Book not available.");
                return;
            }

            if (HasUserBorrowed(username, id))
            {
                Console.WriteLine("You already borrowed this book.");
                return;
            }

            File.AppendAllText(borrowedFile, $"{username},{book.Id},{book.Title}\n");
            book.Quantity--;
            WriteBooks(books);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book borrowed successfully.");
            Console.ResetColor();
        }

        public static void ReturnBook(string username)
        {
            var borrowed = File.ReadAllLines(borrowedFile).ToList();
            var userBooks = borrowed.Where(b => b.StartsWith(username)).ToList();
            if (!userBooks.Any())
            {
                Console.WriteLine("No books to return.");
                return;
            }

            Console.WriteLine("========== Your Borrowed Books ==========");
            foreach (var b in userBooks)
            {
                var parts = b.Split(',');
                Console.WriteLine($"ID: {parts[1]}, Title: {parts[2]}");
            }

            Console.Write("Enter Book ID to return: ");
            string id = Console.ReadLine();
            if (!HasUserBorrowed(username, id))
            {
                Console.WriteLine("You haven't borrowed this book.");
                return;
            }

            borrowed.RemoveAll(b => b.StartsWith($"{username},{id}"));
            File.WriteAllLines(borrowedFile, borrowed);

            var books = ReadBooks();
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.Quantity++;
                WriteBooks(books);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book returned.");
            Console.ResetColor();
        }
    }
}
