namespace LibraryManagement.Infrastructure.Interfaces;

public interface IDbInitializer
{
    void Initialize(LibraryContext context);
}
