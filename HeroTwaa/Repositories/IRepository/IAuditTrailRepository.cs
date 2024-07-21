using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IAuditTrailRepository : IRepository<AuditTrail>
    {
        Task<IEnumerable<AuditTrail>> GetAllAuditTrailsAsync();
        Task<AuditTrail> GetAuditTrailByIdAsync(int id);
        void Update(AuditTrail auditTrail);
    }
}
