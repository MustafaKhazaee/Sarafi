using Sarafi.Application.Interfaces.Services;
using System.Security.Claims;

namespace Sarafi.API.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserService (IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public long GetUserId() =>
            long.Parse(httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.SerialNumber) ?? "0");
    }
}
