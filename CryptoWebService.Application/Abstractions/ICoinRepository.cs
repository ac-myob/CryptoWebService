using CryptoWebService.Domain.Models;
using Transaction = System.Transactions.Transaction;

namespace CryptoWebService.Application.Abstractions;

public interface ICoinRepository
{ 
    Task<IEnumerable<Coin>> GetAllCoinsAsync();
    Task<Coin?> GetCoinByIdAsync(int coinId);
    Task CreateCoinAsync(Coin coin);
    Task<bool> UpdateCoinAsync(Coin updatedCoin);
    Task<bool> DeleteCoinAsync(int coinId);
}
