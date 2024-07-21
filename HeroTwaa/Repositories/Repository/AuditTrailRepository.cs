using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class AuditTrailRepository : Repository<AuditTrail>, IAuditTrailRepository
    {
        public AuditTrailRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AuditTrail>> GetAllAuditTrailsAsync()
        {
            return await ApplicationDbContext.AuditTrails.ToListAsync();
        }

        public async Task<AuditTrail> GetAuditTrailByIdAsync(int id)
        {
            return await ApplicationDbContext.AuditTrails.FindAsync(id);
        }

        public void Update(AuditTrail auditTrail)
        {
            ApplicationDbContext.AuditTrails.Update(auditTrail);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
