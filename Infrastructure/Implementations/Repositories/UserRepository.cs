
using Microsoft.EntityFrameworkCore;
using Sarafi.Application.Extensions;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Application.Interfaces.Services;
using Sarafi.Domain.Entities;
using Sarafi.Infrastructure.Persistence;

namespace Sarafi.Infrastructure.Implementations.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context, IUserService userService) : base(context, userService)
    {
    }

    public async Task<User?> GetUserByUsernameAsync (string username) =>
        await Query.Where(u => u.Username.Equals(username))
                   .Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role)
                   .ThenInclude(r => r.RolePermissions)
                   .ThenInclude(rp => rp.Permission)
                   .AsNoTracking()
                   .FirstOrDefaultAsync();


    public async Task<int> SetRefreshTokenAsync (long userId, string refreshToken) =>
        await UpdateAsync(u => u.Id == userId, u => u.SetProperty(
            u => u.RefreshToken, u => refreshToken
        ));

    public async Task<string> GetRefreshToken(long userId)
    {
        var user = await Query.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return string.Empty;
        }
        return user.RefreshToken;
    }
}
