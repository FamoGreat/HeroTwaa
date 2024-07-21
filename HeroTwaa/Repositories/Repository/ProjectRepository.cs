using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await ApplicationDbContext.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await ApplicationDbContext.Projects.FindAsync(id);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
