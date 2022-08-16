using CryptoWebService.Application.Abstractions;
using CryptoWebService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoWebService.Infrastructure.Repositories;

public class RepositoryBase<TEntities, TKey> : IRepository<TEntities, TKey>
    where TEntities : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TEntities> _entities;

    protected RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<TEntities>();
    }
    
    public async Task<IEnumerable<TEntities>> GetAllAsync()
    {
        return await _entities.AsNoTracking().ToListAsync();
    }

    public async Task<TEntities?> GetByIdAsync(TKey id)
    {
        return await _entities.AsNoTracking().SingleOrDefaultAsync(c => c.Id.Equals(id));
    }

    public async Task CreateAsync(TEntities createdEntity)
    {
        _entities.Add(createdEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(TKey id, TEntities updatedEntity)
    {
        var entityToUpdate = await GetByIdAsync(id);
        if (entityToUpdate == null)
            return false;
        
        _entities.Update(updatedEntity);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(TKey id)
    {
        var entityToDelete = await _entities.FindAsync(id);
        if (entityToDelete == null)
            return false;

        _entities.Remove(entityToDelete);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}