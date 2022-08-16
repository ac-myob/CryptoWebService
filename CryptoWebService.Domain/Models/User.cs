namespace CryptoWebService.Domain.Models;

public class User : IEntity<int>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<Transaction> Transactions { get; set; } = new();
}