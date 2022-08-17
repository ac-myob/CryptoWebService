using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain;
using CryptoWebService.Domain.Models;

namespace CryptoWebService.Infrastructure;

public class ProfitLossCalculator : IProfitLossCalculator
{
    public double GetTotalProfitLoss(IEnumerable<Transaction> transactions)
    {
        var netProfitLoss = 0.0;
        var boughtTransactions = new Queue<Transaction>();
        var orderedTransactionsByDate = transactions.OrderBy(t => t.Date).ToList();

        foreach (var transaction in orderedTransactionsByDate)
        {
            if (transaction.TransactionType == TransactionType.Buy)
                boughtTransactions.Enqueue(transaction);
            else 
                while (transaction.Quantity > 0)
                {
                    var latestBoughtTransaction = boughtTransactions.Peek();
                    if (transaction.Quantity > latestBoughtTransaction.Quantity)
                        boughtTransactions.Dequeue();
                    transaction.Quantity -= Math.Min(latestBoughtTransaction.Quantity, transaction.Quantity);
                    netProfitLoss += (latestBoughtTransaction.Price - transaction.Price) * transaction.Price;
                }
        }

        return netProfitLoss;
    }
}
