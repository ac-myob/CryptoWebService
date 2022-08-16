using CryptoWebService.Application;
using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using CryptoWebService.Infrastructure;
using CryptoWebService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddScoped<RepositoryBase<Coin, int>, CoinRepository>();
builder.Services.AddScoped<IRepository<Coin, int>, CoinRepository>();
builder.Services.AddScoped<IRepository<User, int>, UserRepository>();
builder.Services.AddScoped<IRepository<Transaction, int>, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<DataContext>(options =>
    // options.UseNpgsql("Server=localhost;Port=5432;Database=CryptoDatabase;User Id=Andrew.Christabel;Password=;")
    options.UseInMemoryDatabase("CryptoDatabase"));


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