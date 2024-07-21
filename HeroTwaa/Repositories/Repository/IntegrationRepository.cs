using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class IntegrationRepository : Repository<Integration>, IIntegrationRepository
    {
        public IntegrationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Integration>> GetAllIntegrationsAsync()
        {
            return await ApplicationDbContext.Integrations.ToListAsync();
        }

        public async Task<Integration> GetIntegrationByIdAsync(int id)
        {
            return await ApplicationDbContext.Integrations.FindAsync(id);
        }

        public void Update(Integration integration)
        {
            ApplicationDbContext.Integrations.Update(integration);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
