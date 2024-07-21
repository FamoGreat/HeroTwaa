using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class BillOfQuantitiesRepository : Repository<BillOfQuantities>, IBillOfQuantitiesRepository
    {
        public BillOfQuantitiesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BillOfQuantities>> GetAllBillOfQuantitiesAsync()
        {
            return await ApplicationDbContext.BillOfQuantities.ToListAsync();
        }

        public async Task<BillOfQuantities> GetBillOfQuantitiesByIdAsync(int id)
        {
            return await ApplicationDbContext.BillOfQuantities.FindAsync(id);
        }

        public void Update(BillOfQuantities billOfQuantities)
        {
            ApplicationDbContext.BillOfQuantities.Update(billOfQuantities);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
