using CryptoWebService.Domain.Models;

namespace CryptoWebService.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User, int>
{
    public UserRepository(DataContext dataContext) : base(dataContext) { }
}
