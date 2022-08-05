using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Transaction = System.Transactions.Transaction;

namespace CryptoWebService.Infrastructure.Repositories;

public class CoinRepository : ICoinRepository
{
    private readonly DataContext _dataContext;
    public CoinRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<IEnumerable<Coin>> GetAllCoinsAsync()
    {
        return await _dataContext.Coins.ToListAsync();
    }

    public async Task<Coin?> GetCoinByIdAsync(int coinId)
    {
        return await _dataContext.Coins.FindAsync(coinId);
    }

    public Task<Coin> CreateCoinAsync(Coin coin)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCoinAsync(Coin updatedCoin)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCoinAsync(int coinId)
    {
        throw new NotImplementedException();
    }
}
