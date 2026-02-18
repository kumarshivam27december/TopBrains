using LibraryBookManagementSystem;

var library = new Catalog<Book>();

var book1 = new Book
{
    ISBN = "978-3-16-148410-0",
    Title = "C# Programming",
    Author = "John Sharp",
    Genre = "Programming"
};

library.AddItem(book1);

var programmingBooks = library["Programming"];
Console.WriteLine(programmingBooks.Count); // Should output: 1

var johnsBooks = library.FindBooks(b => b.Author.Contains("John"));
Console.WriteLine(johnsBooks.Count()); // Should output: 1
