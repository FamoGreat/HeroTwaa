using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class ComplianceCheckRepository : Repository<ComplianceCheck>, IComplianceCheckRepository
    {
        public ComplianceCheckRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ComplianceCheck>> GetAllComplianceChecksAsync()
        {
            return await ApplicationDbContext.ComplianceChecks.ToListAsync();
        }

        public async Task<ComplianceCheck> GetComplianceCheckByIdAsync(int id)
        {
            return await ApplicationDbContext.ComplianceChecks.FindAsync(id);
        }

        public void Update(ComplianceCheck complianceCheck)
        {
            ApplicationDbContext.ComplianceChecks.Update(complianceCheck);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
