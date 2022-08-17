using CryptoWebService.Domain.Models;

namespace CryptoWebService.Application.Abstractions;

public interface IProfitLossCalculator
{
    double GetTotalProfitLoss(IEnumerable<Transaction> transactions);
}
