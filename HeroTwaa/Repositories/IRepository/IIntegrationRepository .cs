using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IIntegrationRepository : IRepository<Integration>
    {
        Task<IEnumerable<Integration>> GetAllIntegrationsAsync();
        Task<Integration> GetIntegrationByIdAsync(int id);
        void Update(Integration integration);
    }
}
