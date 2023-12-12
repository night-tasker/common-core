namespace NightTasker.Common.Core.Abstractions;

/// <summary>
/// Дата и время создания сущности.
/// </summary>
public interface ICreatedDateTimeOffset
{
    /// <summary>
    /// Дата и время создания сущности.
    /// </summary>
    DateTimeOffset CreatedDateTimeOffset { get; set; }
}