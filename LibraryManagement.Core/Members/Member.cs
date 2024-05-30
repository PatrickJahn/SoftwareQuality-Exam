namespace LibraryManagement.Core.Members;

public class Member
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public string cprNr { get; set; }

    
    public bool Banned { get; set; }

}