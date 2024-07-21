using HeroTwaa.Models;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IFileUploadRepository : IRepository<FileUpload>
    {
        Task<IEnumerable<FileUpload>> GetAllFileUploadsAsync();
        Task<FileUpload> GetFileUploadByIdAsync(int id);
        void Update(FileUpload fileUpload);
    }
}
