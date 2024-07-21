using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface ITrainingModuleRepository : IRepository<TrainingModule>
    {
        Task<IEnumerable<TrainingModule>> GetAllTrainingModulesAsync();
        Task<TrainingModule> GetTrainingModuleByIdAsync(int id);
        void Update(TrainingModule trainingModule);
    }
}
