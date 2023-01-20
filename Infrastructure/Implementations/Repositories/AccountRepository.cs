
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Application.Interfaces.Services;
using Sarafi.Domain.Entities;
using Sarafi.Infrastructure.Persistence;

namespace Sarafi.Infrastructure.Implementations.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext context, IUserService userService) : base(context, userService)
    {
    }
}
