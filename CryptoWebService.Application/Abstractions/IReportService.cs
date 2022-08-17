using CryptoWebService.Domain.Models;

namespace CryptoWebService.Application.Abstractions;

public interface IReportService
{
    Task<double> GetCoinProfitLossAsync(DateTime startDate, DateTime endDate, int userId, int coinId);
}
