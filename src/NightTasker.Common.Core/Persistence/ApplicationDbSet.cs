using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace NightTasker.Common.Core.Persistence;

/// <summary>
/// Набор записей.
/// </summary>
/// <param name="dbContext">Контекст для работы с данными.</param>
/// <typeparam name="TEntity">Тип сущности записей.</typeparam>
/// <typeparam name="TKey">Тип ключа сущностей записей.</typeparam>
public class ApplicationDbSet<TEntity, TKey>(DbContext dbContext)
    where TEntity : class
{
    private readonly DbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    
    /// <summary>
    /// Набор записей.
    /// </summary>
    private DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

    /// <summary>
    /// Таблица записей определённой сущности. 
    /// </summary>
    public IQueryable<TEntity> Entities => _dbContext.Set<TEntity>();
    
    /// <summary>
    /// Добавить новую запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task Add(TEntity entity, CancellationToken cancellationToken)
    { 
        await DbSet.AddAsync(entity, cancellationToken);
    }

    /// <summary>
    /// Добавить новые записи.
    /// </summary>
    /// <param name="entities">Записи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task AddRange(IReadOnlyCollection<TEntity> entities, CancellationToken cancellationToken)
    {
        return DbSet.AddRangeAsync(entities, cancellationToken);
    }
    
    /// <summary>
    /// Обновить запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }
    
    /// <summary>
    /// Обновить записи.
    /// </summary>
    /// <param name="entities">Записи.</param>
    public void UpdateRange(IReadOnlyCollection<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
    }
    
    /// <summary>
    /// Обновить записи, удовлетворяющие условию.
    /// </summary>
    /// <param name="updateExpression">Условие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task<int> UpdateByExpression(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression, 
        CancellationToken cancellationToken)
    {
        return DbSet.ExecuteUpdateAsync(updateExpression, cancellationToken);
    }

    /// <summary>
    /// Удалить запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    public void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }
    
    /// <summary>
    /// Удалить записи.
    /// </summary>
    /// <param name="entities">Записи.</param>
    public void DeleteRange(IReadOnlyCollection<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }

    /// <summary>
    /// Удалить записи, удовлетворяющие условию.
    /// </summary>
    /// <param name="deleteExpression">Условие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task<int> DeleteByExpression(
        Expression<Func<TEntity, bool>> deleteExpression,
        CancellationToken cancellationToken)
    {
        return DbSet.Where(deleteExpression).ExecuteDeleteAsync(cancellationToken);
    }
    
    /// <summary>
    /// Найти запись по ключу.
    /// </summary>
    /// <param name="key">Ключ.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Запись.</returns>
    public ValueTask<TEntity?> FindAsync(TKey key, CancellationToken cancellationToken)
    {
        return DbSet.FindAsync(key, cancellationToken);
    }
}