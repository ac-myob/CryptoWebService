using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;

namespace CryptoWebService.Application;

public class ReportService : IReportService
{
    private readonly IRepository<Transaction, int> _transactionRepository;
    private readonly IProfitLossCalculator _profitLossCalculator;

    public ReportService(
        IRepository<Transaction, int> transactionRepository, 
        IProfitLossCalculator profitLossCalculator)
    {
        _transactionRepository = transactionRepository;
        _profitLossCalculator = profitLossCalculator;
    }

    public async Task<double> GetCoinProfitLossAsync(DateTime startDate, DateTime endDate, int userId, int coinId)
    {
        var transactions = (await _transactionRepository.GetAllAsync())
            .Where(t => t.UserId == userId)
            .Where(t => t.CoinId == coinId)
            .Where(t => startDate <= t.Date && t.Date <= endDate);

        return _profitLossCalculator.GetTotalProfitLoss(transactions);
    }
}
