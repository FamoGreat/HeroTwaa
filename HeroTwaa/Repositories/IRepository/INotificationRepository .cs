using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        Task<Notification> GetNotificationByIdAsync(int id);
        void Update(Notification notification);
    }
}
