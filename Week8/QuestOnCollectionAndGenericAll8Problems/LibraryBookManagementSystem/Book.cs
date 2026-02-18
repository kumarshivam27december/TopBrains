namespace LibraryBookManagementSystem;

public class Book
{
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
}
