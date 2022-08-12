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
builder.Services.AddScoped<CoinRepository>();
builder.Services.AddScoped<UserRepository>();
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