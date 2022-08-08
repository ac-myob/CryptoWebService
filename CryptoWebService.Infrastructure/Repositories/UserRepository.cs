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
        return await _dataContext.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _dataContext.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId);
    }

    public async Task CreateUserAsync(User user)
    {
        _dataContext.Add(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateUserAsync(User updatedUser)
    {
        var userToUpdate = await GetUserByIdAsync(updatedUser.Id);
        
        if (userToUpdate == null)
            return false;

        _dataContext.Users.Update(updatedUser);
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var userToDelete = await GetUserByIdAsync(userId);

        if (userToDelete == null)
            return false;

        _dataContext.Users.Remove(userToDelete);
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<Transaction>> GetUserTransactionsAsync(int userId)
    {
        return await _dataContext.Transactions.AsNoTracking().Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<Transaction?> GetUserTransactionAsync(int userId, int transactionId)
    {
        return await _dataContext.Transactions
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.UserId == userId && t.Id == transactionId);
    }

    public async Task<bool> CreateUserTransactionAsync(int userId, Transaction transaction)
    {
        var user = await _dataContext.Users
            .Include(u => u.Transactions)
            .SingleOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return false;
        
        user.Transactions.Add(transaction);
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<Transaction?> UpdateUserTransactionAsync(int userId, Transaction updatedTransaction)
    {
        var user = await _dataContext.Users
            .Include(u => u.Transactions)
            .SingleOrDefaultAsync(u => u.Id == userId);
        
        if (user == null)
            return null;

        _dataContext.Transactions.Update(updatedTransaction);
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
