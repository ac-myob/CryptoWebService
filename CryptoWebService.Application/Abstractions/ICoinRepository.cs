using CryptoWebService.Domain.Models;
using Transaction = System.Transactions.Transaction;

namespace CryptoWebService.Application.Abstractions;

public interface ICoinRepository
{ 
    Task<IEnumerable<Coin>> GetAllCoinsAsync();
    Task<Coin?> GetCoinByIdAsync(int coinId);
    Task<Coin> CreateCoinAsync(Coin coin);
    Task<Coin> UpdateCoinAsync(Coin updatedCoin);
    Task<Coin?> DeleteCoinAsync(int coinId);
    
    // Task<IEnumerable<Transaction>> GetCoinTransactionsAsync(int coinId);
    // Task<Transaction> GetCoinTransactionAsync(int coinId, int transactionId);
    // Task CreateCoinTransactionAsync(int coinId, Transaction transaction);
    // Task UpdateCoinTransactionAsync(int coinId, Transaction transaction);
    // Task DeleteCoinTransactionAsync(int coinId, int transactionId);
}
