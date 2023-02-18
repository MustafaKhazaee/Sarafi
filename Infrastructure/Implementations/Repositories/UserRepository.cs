
using Microsoft.EntityFrameworkCore;
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

    public async Task<User?> GetUserByIdAsync(long userId) =>
        await GetUserAllDetails(Query.Where(u => u.Id == userId));

    public async Task<User?> GetUserByUsernameAsync(string username) =>
        await GetUserAllDetails(Query.Where(u => u.Username.Equals(username)));

    public async Task<int> SetRefreshTokenAsync (long userId, string refreshToken) =>
        await UpdateAsync(u => u.Id == userId, u => u.SetProperty(
            u => u.RefreshToken, u => refreshToken
        ));

    private async Task<User?> GetUserAllDetails (IQueryable<User> query) =>
        await query.Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role)
                   .ThenInclude(r => r.RolePermissions)
                   .ThenInclude(rp => rp.Permission)
                   .AsNoTracking()
                   .FirstOrDefaultAsync();
}
