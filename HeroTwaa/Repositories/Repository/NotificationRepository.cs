using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await ApplicationDbContext.Notifications.ToListAsync();
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await ApplicationDbContext.Notifications.FindAsync(id);
        }

        public void Update(Notification notification)
        {
            ApplicationDbContext.Notifications.Update(notification);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
