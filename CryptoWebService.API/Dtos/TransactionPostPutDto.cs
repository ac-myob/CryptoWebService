using CryptoWebService.Domain;

namespace CryptoWebService.API.Dtos;

public class TransactionPostPutDto
{
    public int CoinId { get; set; }
    private TransactionType TransactionType { get; set; }
    public double Quantity { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
}
