namespace LibraryManagement.Core.Lendings;

public class Lending
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime IssuedOn { get; set; }
    public DateTime ShouldBeReturnedBefore { get; set; }
    public DateTime? ReturnedOn { get; set; }
    
}