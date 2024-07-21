using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class TrainingModuleRepository : Repository<TrainingModule>, ITrainingModuleRepository
    {
        public TrainingModuleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TrainingModule>> GetAllTrainingModulesAsync()
        {
            return await ApplicationDbContext.TrainingModules.ToListAsync();
        }

        public async Task<TrainingModule> GetTrainingModuleByIdAsync(int id)
        {
            return await ApplicationDbContext.TrainingModules.FindAsync(id);
        }

        public void Update(TrainingModule trainingModule)
        {
            ApplicationDbContext.TrainingModules.Update(trainingModule);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}