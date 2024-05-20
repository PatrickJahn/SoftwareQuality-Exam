namespace LibraryManagement.Core.Books;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsAvailableForLending { get; set; }
    public int MaxNumberOfLendingDays { get; set; }
    
}