using Sarafi.Application.Interfaces.Services;
using Sarafi.Domain.Constants;
using System.Security.Claims;

namespace Sarafi.API.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserService (IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public long GetUserId() => 
        long.Parse(httpContextAccessor?.HttpContext?.User?.FindFirstValue(UserClaims.Id) ?? "0");

    public string GetUserName() => httpContextAccessor?.HttpContext?.User?.FindFirstValue(UserClaims.Name ?? "");

    public long GetCompanyId() => 1;
        //long.Parse(httpContextAccessor?.HttpContext?.User?.FindFirstValue(UserClaims.CompanyId) ?? "0");
}
