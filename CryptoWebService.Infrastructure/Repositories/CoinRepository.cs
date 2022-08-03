using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Transaction = System.Transactions.Transaction;

namespace CryptoWebService.Infrastructure.Repositories;

public class CoinRepository : ICoinRepository
{
    public Task<IEnumerable<Coin>> GetAllCoinsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Coin> GetCoinByIdAsync(int coinId)
    {
        throw new NotImplementedException();
    }

    public Task CreateCoinAsync(Coin coin)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCoinAsync(Coin updatedCoin)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCoinAsync(int coinId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Transaction>> GetCoinTransactionsAsync(int coinId)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction> GetCoinTransactionAsync(int coinId, int transactionId)
    {
        throw new NotImplementedException();
    }

    public Task CreateCoinTransactionAsync(int coinId, Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCoinTransactionAsync(int coinId, Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCoinTransactionAsync(int coinId, int transactionId)
    {
        throw new NotImplementedException();
    }
}
