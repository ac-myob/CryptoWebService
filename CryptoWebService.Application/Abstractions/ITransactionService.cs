using CryptoWebService.Domain.Models;

namespace CryptoWebService.Application.Abstractions;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetAllAsync();
    Task<Transaction?> GetByIdAsync(int id);
    Task<bool> CreateAsync(Transaction transaction);
    Task<bool> UpdateAsync(int id, Transaction transaction);
    Task<bool> DeleteAsync(int id);
}