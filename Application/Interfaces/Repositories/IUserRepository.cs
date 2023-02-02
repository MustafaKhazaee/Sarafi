
using Microsoft.EntityFrameworkCore;
using Sarafi.Domain.Entities;

namespace Sarafi.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<int> SetRefreshTokenAsync(long userId, string refreshToken);
}
