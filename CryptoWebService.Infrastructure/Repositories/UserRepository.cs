using CryptoWebService.Domain.Models;

namespace CryptoWebService.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User, int>
{
    public UserRepository(DataContext dataContext) : base(dataContext) { }

    // public async Task<IEnumerable<Transaction>> GetUserTransactionsAsync(int userId)
    // {
    //     return await _dataContext.Transactions.AsNoTracking().Where(t => t.UserId == userId).ToListAsync();
    // }
    //
    // public async Task<Transaction?> GetUserTransactionAsync(int userId, int transactionId)
    // {
    //     return await _dataContext.Transactions
    //         .AsNoTracking()
    //         .SingleOrDefaultAsync(t => t.UserId == userId && t.Id == transactionId);
    // }
    //
    // public async Task<bool> CreateUserTransactionAsync(int userId, Transaction createdTransaction)
    // {
    //     var user = await GetUserWithTransactionByIdAsync(userId);
    //     if (user == null)
    //         return false;
    //
    //     createdTransaction.UserId = userId;
    //     _dataContext.Transactions.Add(createdTransaction);
    //     await _dataContext.SaveChangesAsync();
    //
    //     return true;
    // }
    //
    // public async Task<bool> UpdateUserTransactionAsync(int userId, Transaction updatedTransaction)
    // {
    //     var user = await GetUserWithTransactionByIdAsync(userId);
    //     if (user == null)
    //         return false;
    //
    //     updatedTransaction.UserId = userId;
    //     _dataContext.Transactions.Update(updatedTransaction);
    //     await _dataContext.SaveChangesAsync();
    //
    //     return true;
    // }
    //
    // public async Task<Transaction?> DeleteUserTransactionAsync(int userId, int transactionId)
    // {
    //     var transaction = await GetUserTransactionAsync(userId, transactionId);
    //     if (transaction == null)
    //         return null;
    //
    //     _dataContext.Transactions.Remove(transaction);
    //     await _dataContext.SaveChangesAsync();
    //
    //     return transaction;
    // }
}
