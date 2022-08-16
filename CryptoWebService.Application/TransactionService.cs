using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;

namespace CryptoWebService.Application;

public class TransactionService : ITransactionService
{
    private readonly IRepository<Transaction, int> _transactionRepository;
    private readonly IRepository<Coin, int> _coinRepository;
    private readonly IRepository<User, int> _userRepository;

    public TransactionService(
        IRepository<Transaction, int> transactionRepository, 
        IRepository<Coin, int> coinRepository,
        IRepository<User, int> userRepository)
    {
        _transactionRepository = transactionRepository;
        _coinRepository = coinRepository;
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        return await _transactionRepository.GetAllAsync();
    }

    public async Task<Transaction?> GetByIdAsync(int id)
    {
        return await _transactionRepository.GetByIdAsync(id);
    }

    public async Task<bool> CreateAsync(Transaction transaction)
    {
        if (! await IsTransactionValid(transaction.CoinId, transaction.UserId))
            return false;

        await _transactionRepository.CreateAsync(transaction);

        return true;
    }

    public async Task<bool> UpdateAsync(int id, Transaction transaction)
    {
        if (! await IsTransactionValid(transaction.CoinId, transaction.UserId))
            return false;

        await _transactionRepository.UpdateAsync(id, transaction);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _transactionRepository.DeleteAsync(id);
    }

    private async Task<bool> IsTransactionValid(int coinId, int userId)
    {
        var coinExists = await _coinRepository.GetByIdAsync(coinId) != null;
        var userExists = await _userRepository.GetByIdAsync(userId) != null;

        return coinExists && userExists;
    }
}