using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IBillOfQuantitiesRepository : IRepository<BillOfQuantities>
    {
        Task<IEnumerable<BillOfQuantities>> GetAllBillOfQuantitiesAsync();
        Task<BillOfQuantities> GetBillOfQuantitiesByIdAsync(int id);
        void Update(BillOfQuantities billOfQuantities);
    }
}
