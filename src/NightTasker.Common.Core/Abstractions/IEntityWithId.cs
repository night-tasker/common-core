namespace NightTasker.Common.Core.Abstractions;

/// <summary>
/// Сущность с ИД.
/// </summary>
/// <typeparam name="T">Тип ИД.</typeparam>
public interface IEntityWithId<T> : IEntity
{
    /// <summary>
    /// ИД.
    /// </summary>
    T Id { get; set; }
}