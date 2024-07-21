using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public ReportRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return await ApplicationDbContext.Reports.ToListAsync();
        }

        public async Task<Report> GetReportByIdAsync(int id)
        {
            return await ApplicationDbContext.Reports.FindAsync(id);
        }

        public void Update(Report report)
        {
            ApplicationDbContext.Reports.Update(report);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
