namespace CryptoWebService.Domain.Models;

public class Coin
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public List<Transaction> Transactions { get; set; }
}