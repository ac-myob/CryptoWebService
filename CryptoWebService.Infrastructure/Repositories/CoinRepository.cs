using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Coin> CreateCoinAsync(Coin coin)
    {
        _dataContext.Add(coin);
        await _dataContext.SaveChangesAsync();

        return coin;
    }

    public async Task<Coin> UpdateCoinAsync(Coin updatedCoin)
    {
        _dataContext.Coins.Update(updatedCoin);
        await _dataContext.SaveChangesAsync();

        return updatedCoin;
    }

    public async Task<Coin?> DeleteCoinAsync(int coinId)
    {
        var coinToDelete = await _dataContext.Coins.FindAsync(coinId);

        if (coinToDelete == null)
            return null;

        _dataContext.Remove(coinToDelete);
        await _dataContext.SaveChangesAsync();

        return coinToDelete;
    }
}
