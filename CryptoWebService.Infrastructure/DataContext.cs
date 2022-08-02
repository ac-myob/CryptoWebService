using CryptoWebService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Transaction = System.Transactions.Transaction;

namespace CryptoWebService.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Coin> Coins { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}