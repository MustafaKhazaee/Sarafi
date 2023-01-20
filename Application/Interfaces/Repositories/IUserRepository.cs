
using Sarafi.Domain.Entities;

namespace Sarafi.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByUsernameAsync(string username);

}
