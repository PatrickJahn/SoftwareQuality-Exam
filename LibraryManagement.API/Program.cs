using LibraryManagement.Core.Books;
using LibraryManagement.Core.Common.Interfaces;
using LibraryManagement.Infrastructure;
using LibraryManagement.Infrastructure.Interfaces;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<LibraryContext>(opt => opt.UseInMemoryDatabase("HotelBookingDb"));
builder.Services.AddTransient<IDbInitializer, DbInitializer>();


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
