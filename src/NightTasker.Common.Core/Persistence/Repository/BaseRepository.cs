using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NightTasker.Common.Core.Abstractions;

namespace NightTasker.Common.Core.Persistence.Repository;

/// <inheritdoc cref="IRepository{TEntity,TKey}"/>
public abstract class BaseRepository<TEntity, TKey>(DbContext dbContext) : IRepository<TEntity, TKey>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Набор записей.
    /// </summary>
    private readonly ApplicationDbSet<TEntity, TKey> _dbSet = new(dbContext);
    
    /// <inheritdoc />
    public IQueryable<TEntity> Entities => _dbSet.Entities;
    
    /// <inheritdoc />
    public async Task<TEntity?> TryGetById(TKey entityId, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(entityId, cancellationToken);
    }

    /// <inheritdoc />
    public virtual Task<List<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return Entities.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task Add(TEntity entity, CancellationToken cancellationToken)
    { 
        return _dbSet.Add(entity, cancellationToken);
    }
    
    /// <inheritdoc />
    public Task AddRange(IReadOnlyCollection<TEntity> entities, CancellationToken cancellationToken)
    {
        return _dbSet.AddRange(entities, cancellationToken);
    }
    
    /// <inheritdoc />
    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }
    
    /// <inheritdoc />
    public void UpdateRange(IReadOnlyCollection<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    /// <inheritdoc />
    public virtual Task UpdateByExpression(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression, 
        CancellationToken cancellationToken)
    {
        return _dbSet.UpdateByExpression(updateExpression, cancellationToken);
    }
    
    /// <inheritdoc />
    public void Delete(TEntity entity)
    {
        _dbSet.Delete(entity);
    }
    
    /// <inheritdoc />
    public void DeleteRange(IReadOnlyCollection<TEntity> entities)
    {
        _dbSet.DeleteRange(entities);
    }

    /// <inheritdoc />
    public virtual async Task DeleteByExpression(
        Expression<Func<TEntity, bool>> deleteExpression,
        CancellationToken cancellationToken)
    {
        await _dbSet.DeleteByExpression(deleteExpression, cancellationToken);
    }
}