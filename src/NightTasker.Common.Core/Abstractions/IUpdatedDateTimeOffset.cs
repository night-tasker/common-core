namespace NightTasker.Common.Core.Abstractions;

/// <summary>
/// Дата и время обновления.
/// </summary>
public interface IUpdatedDateTimeOffset
{
    /// <summary>
    /// Дата и время обновления.
    /// </summary>
    DateTimeOffset? UpdatedDateTimeOffset { get; set; }
}