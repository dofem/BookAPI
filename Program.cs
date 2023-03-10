using BookAPI.Data;
using BookAPI.Model;
using BookAPI.Service.Implementation;
using BookAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{ options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });
builder.Services.AddScoped<IPublisherRepository,PublisherRepository>();
builder.Services.AddScoped<IRepository<Book>,BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
