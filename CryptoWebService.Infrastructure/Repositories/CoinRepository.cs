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
        return await _dataContext.Coins.AsNoTracking().ToListAsync();
    }

    public async Task<Coin?> GetCoinByIdAsync(int coinId)
    {
        return await _dataContext.Coins.AsNoTracking().SingleOrDefaultAsync(c => c.Id == coinId);
    }

    public async Task CreateCoinAsync(Coin coin)
    {
        _dataContext.Add(coin);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateCoinAsync(Coin updatedCoin)
    {
        // Multiple options to update resource
        
        // 1) Use AsNoTracking (current implementation)
        if (await GetCoinByIdAsync(updatedCoin.Id) == null)
            return false;
        
        _dataContext.Coins.Update(updatedCoin);
        await _dataContext.SaveChangesAsync();

        return true;
        
        // 2) Use SetValues
        // var coinToUpdate = await GetCoinByIdAsync(updatedCoin.Id);
        // if (coinToUpdate == null)
        //     return false;
        //
        // _dataContext.Entry(coinToUpdate).CurrentValues.SetValues(updatedCoin);
        // await _dataContext.SaveChangesAsync();
        //
        // return true;
    }

    public async Task<bool> DeleteCoinAsync(int coinId)
    {
        var coinToDelete = await _dataContext.Coins.FindAsync(coinId);

        if (coinToDelete == null)
            return false;

        _dataContext.Remove(coinToDelete);
        await _dataContext.SaveChangesAsync();

        return true;
    }
}
