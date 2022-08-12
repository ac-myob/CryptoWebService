namespace CryptoWebService.Domain.Models;

public class User : IEntity<int>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Transaction> Transactions { get; set; }
}