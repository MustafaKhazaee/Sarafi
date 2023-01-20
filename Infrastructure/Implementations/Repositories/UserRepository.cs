
using Microsoft.EntityFrameworkCore;
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Entities;
using Sarafi.Infrastructure.Persistence;

namespace Sarafi.Infrastructure.Implementations.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext _context) : base(_context)
    {
    }

    public async Task<User?> GetUserByUsernameAsync(string username) =>
        await Query.Where(u => u.Username.Equals(username))
                   .Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role)
                   .ThenInclude(r => r.RolePermissions)
                   .ThenInclude(rp => rp.Permission)
                   .AsNoTracking()
                   .FirstOrDefaultAsync();
}
