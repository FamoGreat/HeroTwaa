using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        void Update(Project project);
    }
}
