using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class ApprovalRepository : Repository<Approval>, IApprovalRepository
    {
        public ApprovalRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Approval>> GetAllApprovalsAsync()
        {
            return await ApplicationDbContext.Approvals.ToListAsync();
        }

        public async Task<Approval> GetApprovalByIdAsync(int id)
        {
            return await ApplicationDbContext.Approvals.FindAsync(id);
        }

        public void Update(Approval approval)
        {
            ApplicationDbContext.Approvals.Update(approval);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
