namespace NightTasker.Common.Core.Identity.Contracts;

/// <summary>
/// Сервис для работы с аутентификацией.
/// </summary>
public interface IIdentityService
{
    /// <summary>
    /// ИД текущего пользователя.
    /// </summary>
    Guid? CurrentUserId { get; }
    
    /// <summary>
    /// Аутентифицирован ли текущий пользователь.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Происходит ли выполнение системой (не пользовательский запрос).
    /// </summary>
    public bool IsSystem { get; set; }
}