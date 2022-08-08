using CryptoWebService.Domain.Models;

namespace CryptoWebService.Application.Abstractions;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(int userId);
    Task CreateUserAsync(User user);
    Task<bool> UpdateUserAsync(User updatedUser);
    Task<bool> DeleteUserAsync(int userId);
    
    Task<IEnumerable<Transaction>> GetUserTransactionsAsync(int userId);
    Task<Transaction?> GetUserTransactionAsync(int userId, int transactionId);
    Task<bool> CreateUserTransactionAsync(int userId, Transaction transaction);
    Task<Transaction?> UpdateUserTransactionAsync(int userId, Transaction updatedTransaction);
    Task<Transaction?> DeleteUserTransactionAsync(int userId, int transactionId);
}
