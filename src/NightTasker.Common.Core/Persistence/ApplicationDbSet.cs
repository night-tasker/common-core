using Microsoft.EntityFrameworkCore;

namespace NightTasker.Common.Core.Persistence;

/// <summary>
/// Набор записей.
/// </summary>
/// <param name="dbContext">Контекст для работы с данными.</param>
/// <typeparam name="TEntity">Тип сущности записей.</typeparam>
/// <typeparam name="TKey">Тип ключа сущностей записей.</typeparam>
public class ApplicationDbSet<TEntity, TKey>(
    DbContext dbContext, 
    IQueryable<TEntity>? entities = null)
    where TEntity : class
{
    /// <summary>
    /// Контекст для работы с данными.
    /// </summary>
    private readonly DbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    /// <summary>
    /// Таблица записей определённой сущности. 
    /// </summary>
    public IQueryable<TEntity> Entities => entities ?? _dbContext.Set<TEntity>();
    
    /// <summary>
    /// Добавить новую запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public async Task Add(TEntity entity, CancellationToken cancellationToken)
    { 
        await _dbContext.AddAsync(entity, cancellationToken);
    }

    /// <summary>
    /// Добавить новые записи.
    /// </summary>
    /// <param name="entities">Записи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task AddRange(IReadOnlyCollection<TEntity> entities, CancellationToken cancellationToken)
    {
        return _dbContext.AddRangeAsync(entities, cancellationToken);
    }
    
    /// <summary>
    /// Обновить запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    public void Update(TEntity entity)
    {
        _dbContext.Update(entity);
    }
    
    /// <summary>
    /// Обновить записи.
    /// </summary>
    /// <param name="entities">Записи.</param>
    public void UpdateRange(IReadOnlyCollection<TEntity> entities)
    {
        _dbContext.UpdateRange(entities);
    }

    /// <summary>
    /// Удалить запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    public void Delete(TEntity entity)
    {
        _dbContext.Remove(entity);
    }
    
    /// <summary>
    /// Удалить записи.
    /// </summary>
    /// <param name="entities">Записи.</param>
    public void DeleteRange(IReadOnlyCollection<TEntity> entities)
    {
        _dbContext.RemoveRange(entities);
    }
}