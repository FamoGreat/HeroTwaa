using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class PublicDashboardRepository : Repository<PublicDashboard>, IPublicDashboardRepository
    {
        public PublicDashboardRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PublicDashboard>> GetAllPublicDashboardsAsync()
        {
            return await ApplicationDbContext.PublicDashboards.ToListAsync();
        }

        public async Task<PublicDashboard> GetPublicDashboardByIdAsync(int id)
        {
            return await ApplicationDbContext.PublicDashboards.FindAsync(id);
        }

        public void Update(PublicDashboard publicDashboard)
        {
            ApplicationDbContext.PublicDashboards.Update(publicDashboard);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
