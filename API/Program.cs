using DAL;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DLL.Services.Interfaces;
using DLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
//options.UseSqlServer(connection, b => b.MigrationsAssembly("API")).By default,
options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnectionString")));



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
