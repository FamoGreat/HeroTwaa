using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IBoqItemRepository : IRepository<BoqItem>
    {
        Task<IEnumerable<BoqItem>> GetAllBoqItemsAsync();
        Task<BoqItem> GetBoqItemByIdAsync(int id);
        void Update(BoqItem boqItem);
    }
}
