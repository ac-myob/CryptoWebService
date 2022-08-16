using CryptoWebService.Domain.Models;

namespace CryptoWebService.Infrastructure.Repositories;

public class TransactionRepository : RepositoryBase<Transaction, int>
{
    public TransactionRepository(DataContext dataContext) : base(dataContext) { }
}