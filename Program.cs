using Microsoft.EntityFrameworkCore;
using Reservation.Data;

var builder = WebApplication.CreateBuilder(args);
SQLitePCL.Batteries_V2.Init();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin() // Allows requests from any origin
            .AllowAnyHeader() // Allows any headers
            .AllowAnyMethod()); // Allows any HTTP methods
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();