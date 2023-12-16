using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NightTasker.Common.Core.Abstractions;

namespace NightTasker.Common.Core.Persistence.Repository;

/// <inheritdoc cref="IRepository{TEntity,TKey}"/>
public abstract class BaseRepository<TEntity, TKey, TDbContext>(TDbContext context) : IRepository<TEntity, TKey>
    where TEntity : class, IEntity where TDbContext : DbContext
{
    /// <summary>
    /// Записи определённой сущности.
    /// </summary>
    private DbSet<TEntity> Entities { get; init; } = context.Set<TEntity>();

    /// <summary>
    /// Таблица записей определённой сущности.
    /// </summary>
    public virtual IQueryable<TEntity> Table => Entities;

    /// <summary>
    /// Неотслеживаемая таблица определённой сущности.
    /// </summary>
    public virtual IQueryable<TEntity> NoTrackingTable => Entities.AsNoTrackingWithIdentityResolution();

    /// <summary>
    /// Получить все записи.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список всех записей.</returns>
    public virtual async Task<List<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await Entities.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Добавить новую запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public virtual async Task Add(TEntity entity, CancellationToken cancellationToken)
    { 
        await Entities.AddAsync(entity, cancellationToken);
    }

    /// <summary>
    /// Обновить записи, удовлетворяющие условию.
    /// </summary>
    /// <param name="updateExpression">Условие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public virtual async Task UpdateByExpression(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression, 
        CancellationToken cancellationToken)
    {
        await Entities.ExecuteUpdateAsync(updateExpression, cancellationToken);
    }

    /// <summary>
    /// Удалить записи, удовлетворяющие условию.
    /// </summary>
    /// <param name="deleteExpression">Условие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public virtual async Task DeleteByExpression(
        Expression<Func<TEntity, bool>> deleteExpression,
        CancellationToken cancellationToken)
    {
        await Entities.Where(deleteExpression).ExecuteDeleteAsync(cancellationToken);
    }

    /// <summary>
    /// Попробовать получить запись по ИД.
    /// </summary>
    /// <param name="entityId">ИД.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>Запись.</returns>
    public async Task<TEntity?> TryGetById(TKey entityId, CancellationToken cancellationToken)
    {
        var entity = await Entities.FindAsync(entityId);
        return entity;
    }
}