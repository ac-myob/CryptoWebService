using CryptoWebService.Domain.Models;

namespace CryptoWebService.Application.Abstractions;

public interface IRepository<TEntities, in TKey> 
    where TEntities : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    Task<IEnumerable<TEntities>> GetAllAsync();
    Task<TEntities?> GetByIdAsync(TKey id);
    Task CreateAsync(TEntities entity);
    Task<bool> UpdateAsync(TKey id, TEntities entity);
    Task<bool> DeleteAsync(TKey id);
}