using CryptoWebService.Domain.Models;

namespace CryptoWebService.Infrastructure.Repositories;

public class CoinRepository : RepositoryBase<Coin, int>
{
    public CoinRepository(DataContext dataContext) : base(dataContext) { }
}
