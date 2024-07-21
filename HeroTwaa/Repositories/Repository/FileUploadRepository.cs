using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories.Repository
{
    public class FileUploadRepository : Repository<FileUpload>, IFileUploadRepository
    {
        public FileUploadRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FileUpload>> GetAllFileUploadsAsync()
        {
            return await ApplicationDbContext.FileUploads.ToListAsync();
        }

        public async Task<FileUpload> GetFileUploadByIdAsync(int id)
        {
            return await ApplicationDbContext.FileUploads.FindAsync(id);
        }

        public void Update(FileUpload fileUpload)
        {
            ApplicationDbContext.FileUploads.Update(fileUpload);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
    }
}
