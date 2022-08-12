using CryptoWebService.Domain;

namespace CryptoWebService.API.Dtos;

public class TransactionPostPutDto
{
    public int UserId { get; set; }
    public int CoinId { get; set; }
    public TransactionType TransactionType { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
}
