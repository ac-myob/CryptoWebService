using CryptoWebService.Domain;

namespace CryptoWebService.API.Dtos;

public class TransactionGetDto
{
    public int Id { get; set; }
    private TransactionType TransactionType { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
}