using System.Reflection;
using CryptoWebService.API.Dtos;
using CryptoWebService.Application;
using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using CryptoWebService.Infrastructure;
using CryptoWebService.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CoinPostPutDto>();
builder.Services.AddValidatorsFromAssemblyContaining<TransactionPostPutDto>();
builder.Services.AddValidatorsFromAssemblyContaining<TransactionGetDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UserPostPutDto>();

// Repositories/Services
builder.Services.AddScoped<IRepository<Coin, int>, CoinRepository>();
builder.Services.AddScoped<IRepository<User, int>, UserRepository>();
builder.Services.AddScoped<IRepository<Transaction, int>, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSingleton<IProfitLossCalculator, ProfitLossCalculator>();

// DTO mapping
builder.Services.AddAutoMapper(typeof(Program));

// DbContext
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