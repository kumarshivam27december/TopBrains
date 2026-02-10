using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement
{
    class Program
    {
        static List<dynamic> books = new List<dynamic>();

        static void Main(string[] args)
        {
            Console.WriteLine("BOOK LIBRARY MANAGEMENT SYSTEM\n");

            AddBook(1, "C# Basics", "Microsoft", 450);
            AddBook(2, "ASP.NET Core", "Microsoft", 650);
            AddBook(3, "Java Fundamentals", "Oracle", 500);
            AddBook(4, "Python Guide", "OReilly", 300);

            UpdateBook(3, "Java Advanced", "Oracle", 550);
            DeleteBook(4);

            ViewAllBooks();
            SearchByName("C#");
            SearchByPublisher("Microsoft");
            ShowHighestPricedBook();
            ShowLowestPricedBook();
        }


        public static void AddBook(int id, string name, string publisher, decimal price)
        {
            dynamic book = new System.Dynamic.ExpandoObject();
            book.BookId = id;
            book.BookName = name;
            book.Publisher = publisher;
            book.Price = price;

            books.Add(book);
            Console.WriteLine($"Book Added: {book.BookName}");
        }

        public static void UpdateBook(int id, string name, string publisher, decimal price)
        {
            foreach (var book in books)
            {
                if (book.BookId == id)
                {
                    book.BookName = name;
                    book.Publisher = publisher;
                    book.Price = price;
                    Console.WriteLine($"Book Updated: ID {id}");
                    return;
                }
            }
        }

        public static void DeleteBook(int id)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].BookId == id)
                {
                    Console.WriteLine($"Book Deleted: {books[i].BookName}");
                    books.RemoveAt(i);
                    return;
                }
            }
        }


        public static void ViewAllBooks()
        {
            Console.WriteLine("\nAll Books:");
            foreach (var book in books)
            {
                DisplayBook(book);
            }
        }

        public static void SearchByName(string name)
        {
            Console.WriteLine($"\nSearch By Book Name: {name}");
            foreach (var book in books)
            {
                if (book.BookName.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    DisplayBook(book);
                }
            }
        }

        public static void SearchByPublisher(string publisher)
        {
            Console.WriteLine($"\nSearch By Publisher: {publisher}");
            foreach (var book in books)
            {
                if (book.Publisher.Equals(publisher, StringComparison.OrdinalIgnoreCase))
                {
                    DisplayBook(book);
                }
            }
        }

        public static void ShowHighestPricedBook()
        {
            var book = books.OrderByDescending(b => b.Price).First();
            Console.WriteLine("\nHighest Priced Book:");
            DisplayBook(book);
        }

        public static void ShowLowestPricedBook()
        {
            var book = books.OrderBy(b => b.Price).First();
            Console.WriteLine("\nLowest Priced Book:");
            DisplayBook(book);
        }


        public static void DisplayBook(dynamic book)
        {
            Console.WriteLine($"ID: {book.BookId}, Name: {book.BookName}, Publisher: {book.Publisher}, Price: {book.Price}");
        }
    }
}
