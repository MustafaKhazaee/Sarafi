
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Domain.Entities;
using Sarafi.Infrastructure.Persistence;

namespace Sarafi.Infrastructure.Implementations.Repositories;

public class ActivityRepository : Repository<Activity>, IActivityRepository
{
    public ActivityRepository(ApplicationDbContext _context) : base(_context)
    {
    }
}
