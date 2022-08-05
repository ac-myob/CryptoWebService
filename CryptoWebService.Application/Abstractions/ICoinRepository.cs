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
}
