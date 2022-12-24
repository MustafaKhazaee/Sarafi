
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Entities;
using Sarafi.Infrastructure.Persistence;

namespace Sarafi.Infrastructure.Implementations.Repositories
{
    public class MasterAccountRepository : Repository<MasterAccount>, IMasterAccountRepository
    {
        public MasterAccountRepository(ApplicationDbContext _context) : base(_context)
        {
        }
    }
}
