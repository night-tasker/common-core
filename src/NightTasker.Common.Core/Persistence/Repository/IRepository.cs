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
    /// Удалить запись.
    /// </summary>
    /// <param name="entity">Запись.</param>
    void Delete(TEntity entity);

    /// <summary>
    /// Удалить записи.
    /// </summary>
    /// <param name="entities">Записи.</param>
    void DeleteRange(IReadOnlyCollection<TEntity> entities);
}