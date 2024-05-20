using LibraryManagement.Application.Interfaces;
using LibraryManagement.Application.Services;
using LibraryManagement.Core.Books;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Core.Lendings;
using LibraryManagement.Core.Members;
using LibraryManagement.Infrastructure;
using LibraryManagement.Infrastructure.Interfaces;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<LibraryContext>(opt => opt.UseInMemoryDatabase("HotelBookingDb"));
builder.Services.AddTransient<IDbInitializer, DbInitializer>();
builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<IRepository<Member>, MemberRepository>();
builder.Services.AddScoped<IRepository<Lending>, LendingRepository>();

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IMemberService, MemberService>();
builder.Services.AddTransient<ILendingService, LendingService>();


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Initialize the database.
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetService<LibraryContext>();
        var dbInitializer = services.GetService<IDbInitializer>();
        dbInitializer.Initialize(dbContext);
    }
}

app.UseHttpsRedirection();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
