using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IApprovalRepository : IRepository<Approval>
    {
        Task<IEnumerable<Approval>> GetAllApprovalsAsync();
        Task<Approval> GetApprovalByIdAsync(int id);
        void Update(Approval approval);
    }
}
