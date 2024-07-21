using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IComplianceCheckRepository : IRepository<ComplianceCheck>
    {
        Task<IEnumerable<ComplianceCheck>> GetAllComplianceChecksAsync();
        Task<ComplianceCheck> GetComplianceCheckByIdAsync(int id);
        void Update(ComplianceCheck complianceCheck);
    }
}
