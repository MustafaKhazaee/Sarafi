
using Sarafi.Application.Interfaces.Repositories;
using Sarafi.Application.Interfaces.Services;
using Sarafi.Domain.Entities;
using Sarafi.Infrastructure.Persistence;

namespace Sarafi.Infrastructure.Implementations.Repositories;

public class NotificationRepository : Repository<Notification>, INotificationRepository
{
    public NotificationRepository(ApplicationDbContext context, IUserService userService) : base(context, userService)
    {
    }
}
