using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using NightTasker.Common.Core.Identity.Contracts;

namespace NightTasker.Common.Core.Identity.Implementations;

/// <inheritdoc />
public class IdentityService : IIdentityService
{
    public IdentityService(
        IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor is null)
        {
            throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        
        if (httpContextAccessor.HttpContext is null)
        {
            IsSystem = true;
            return;
        }
        
        if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
        {
            return;
        }
        
        var currentUserIdString =
            httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (String.IsNullOrEmpty(currentUserIdString))
        {
            return;
        }
        
        CurrentUserId = Guid.Parse(currentUserIdString);
        IsAuthenticated = true;
    }

    /// <inheritdoc />
    public Guid? CurrentUserId { get; }

    /// <inheritdoc />
    public bool IsAuthenticated { get; }
    
    /// <inheritdoc />
    public bool IsSystem { get; set; }
}