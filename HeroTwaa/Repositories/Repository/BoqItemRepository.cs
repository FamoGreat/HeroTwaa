using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class BoqItemRepository : Repository<BoqItem>, IBoqItemRepository
    {
        public BoqItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BoqItem>> GetAllBoqItemsAsync()
        {
            return await ApplicationDbContext.BoqItems.ToListAsync();
        }

        public async Task<BoqItem> GetBoqItemByIdAsync(int id)
        {
            return await ApplicationDbContext.BoqItems.FindAsync(id);
        }

        public void Update(BoqItem boqItem)
        {
            ApplicationDbContext.BoqItems.Update(boqItem);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
