using CryptoWebService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWebService.Infrastructure.Repositories;

public class CoinRepository : IRepository<Coin>
{
    private readonly DataContext _dataContext;

    public CoinRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public void Create(Coin entity)
    {
        _dataContext.Add(entity);
        _dataContext.SaveChanges();
    }

    public IEnumerable<Coin> ReadAll()
    {
        return _dataContext.Coins;
    }

    public Coin? Read(int id)
    {
        return _dataContext.Coins.Find(id);
    }

    public void Update(Coin entity)
    {
        _dataContext.Entry(entity).State = EntityState.Modified;
        _dataContext.SaveChanges();
    }

    public bool Delete(int id)
    {
        var transactionToRemove = Read(id);

        if (transactionToRemove == null)
            return false;
        
        _dataContext.Remove(transactionToRemove);
        _dataContext.SaveChanges();

        return true; 
    }
}