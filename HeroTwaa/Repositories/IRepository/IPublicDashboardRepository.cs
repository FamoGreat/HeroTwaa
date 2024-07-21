using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IPublicDashboardRepository : IRepository<PublicDashboard>
    {
        Task<IEnumerable<PublicDashboard>> GetAllPublicDashboardsAsync();
        Task<PublicDashboard> GetPublicDashboardByIdAsync(int id);
        void Update(PublicDashboard publicDashboard);
    }
}
