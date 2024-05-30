namespace LibraryManagement.Controllers.Dtos;

public class LendRequest
{
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
}