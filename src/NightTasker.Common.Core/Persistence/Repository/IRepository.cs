using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace NightTasker.Common.Core.Persistence.Repository;

/// <summary>
/// Репозиторий.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
/// <typeparam name="TKey">Тип ключа сущности.</typeparam>
public interface IRepository<TEntity, TKey> where TEntity : class
{
    /// <summary>
    /// Таблица записей определённой сущности.
    /// </summary>
    IQueryable<TEntity> Entities { get; }
    
    /// <summary>
    /// Попробовать получить запись по ИД.
    /// </summary>
    /// <param name="entityId">ИД.</param>
    /// <param name="cancellationToken">Токен.</param>
    /// <returns>Запись.</returns>
    Task<TEntity?> TryGetById(TKey entityId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить все записи.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список всех записей.</returns>
    Task<List<TEntity>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Добавить новую запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task Add(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить новые записи.
    /// </summary>
    /// <param name="entities">Записи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task AddRange(IReadOnlyCollection<TEntity> entities, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Обновить записи.
    /// </summary>
    /// <param name="entities">Записи.</param>
    void UpdateRange(IReadOnlyCollection<TEntity> entities);

    /// <summary>
    /// Обновить записи, удовлетворяющие условию.
    /// </summary>
    /// <param name="updateExpression">Условие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task UpdateByExpression(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression,
        CancellationToken cancellationToken);

    /// <summary>
    /// Удалить запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Удалить записи.
    /// </summary>
    /// <param name="entities">Записи.</param>
    void DeleteRange(IReadOnlyCollection<TEntity> entities);
    
    /// <summary>
    /// Удалить записи, удовлетворяющие условию.
    /// </summary>
    /// <param name="deleteExpression">Условие.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task DeleteByExpression(
        Expression<Func<TEntity, bool>> deleteExpression,
        CancellationToken cancellationToken);
}