namespace NightTasker.Common.Core.Exceptions.Models;

/// <summary>
/// Детали ошибки (исключения).
/// </summary>
public class ErrorDetails
{
    /// <summary>
    /// Сообщение.
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// Сообщение для отображения.
    /// </summary>
    public string? DisplayMessage { get; set; }

    /// <summary>
    /// Trace-ИД.
    /// </summary>
    public string TraceId { get; set; } = null!;

    /// <summary>
    /// Стандартное сообщение ошибки.
    /// </summary>
    public const string DefaultErrorMessage = "An error occurred while processing your request.";
}