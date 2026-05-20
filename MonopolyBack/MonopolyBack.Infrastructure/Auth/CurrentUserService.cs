using Microsoft.AspNetCore.Http;
using MonopolyBack.Application.Abstractions.Auth;

namespace MonopolyBack.Infrastructure.Auth;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    public Guid? GetUserId()
    {
        if (_httpContextAccessor.HttpContext?.Items.TryGetValue("UserId", out var idObj) == true && idObj is Guid userId)
            return userId;
        return null;
    }
}