namespace CryptoWebService.Domain.Models;

public class Coin : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
}