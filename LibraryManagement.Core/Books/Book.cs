namespace LibraryManagement.Core.Books;

public class Book
{
    public Guid Id { get; set; } = new Guid();
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsAvailable { get; set; }
    public int MaxNumberOfLendingDays { get; set; }
    
}