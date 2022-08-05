using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWebService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _dataContext.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _dataContext.Users.FindAsync(userId);
    }

    public async Task CreateUserAsync(User user)
    {
        _dataContext.Add(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<User?> UpdateUserAsync(User updatedUser)
    {
        if (await _dataContext.Users.FindAsync(updatedUser) == null)
            return null;
        
        _dataContext.Users.Update(updatedUser);
        await _dataContext.SaveChangesAsync();

        return updatedUser;
    }

    public async Task<User?> DeleteUserAsync(int userId)
    {
        var userToDelete = await _dataContext.Users.FindAsync(userId);

        if (userToDelete == null)
            return null;

        _dataContext.Users.Remove(userToDelete);
        await _dataContext.SaveChangesAsync();

        return userToDelete;
    }

    public async Task<IEnumerable<Transaction>> GetUserTransactionsAsync(int userId)
    {
        return await _dataContext.Transactions.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Transaction?> GetUserTransactionAsync(int userId, int transactionId)
    {
        return await _dataContext.Transactions
            .SingleOrDefaultAsync(t => t.UserId == userId && t.Id == transactionId);
    }

    public async Task<Transaction?> CreateUserTransactionAsync(int userId, Transaction transaction)
    {
        var user = await _dataContext.Users
            .Include(u => u.Transactions)
            .SingleOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return null;
        
        user.Transactions.Add(transaction);
        await _dataContext.SaveChangesAsync();

        return transaction;
    }

    public async Task<Transaction?> UpdateUserTransactionAsync(int userId, Transaction updatedTransaction)
    {
        var user = await _dataContext.Users
            .Include(u => u.Transactions)
            .SingleOrDefaultAsync(u => u.Id == userId);
        
        if (user == null)
            return null;

        var updateIndex = user.Transactions.FindIndex(t => t.Id == updatedTransaction.Id);
        user.Transactions[updateIndex] = updatedTransaction;
        await _dataContext.SaveChangesAsync();

        return updatedTransaction;
    }

    public async Task<Transaction?> DeleteUserTransactionAsync(int userId, int transactionId)
    {
        var transaction = await GetUserTransactionAsync(userId, transactionId);
        if (transaction == null)
            return null;

        _dataContext.Transactions.Remove(transaction);
        await _dataContext.SaveChangesAsync();

        return transaction;
    }
}
