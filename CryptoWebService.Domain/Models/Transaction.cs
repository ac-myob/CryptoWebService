namespace CryptoWebService.Domain.Models;

public class Transaction
{
    public int Id { get; set; }
    private TransactionType TransactionType { get; set; }
    public Coin Coin { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
}