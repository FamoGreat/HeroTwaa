using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IReportRepository : IRepository<Report>
    {
        Task<IEnumerable<Report>> GetAllReportsAsync();
        Task<Report> GetReportByIdAsync(int id);
        void Update(Report report);
    }
}
