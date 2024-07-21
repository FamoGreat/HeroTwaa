using HeroTwaa.Models;
using Microsoft.AspNetCore.Identity;

namespace HeroTwaa.Repositories.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IdentityResult> RegisterUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> RegisterUsersAsync(IEnumerable<ApplicationUser> users, string password);
        Task<SignInResult> LoginUserAsync(string email, string password);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task LogoutUserAsync();
        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<IdentityResult> RemoveFromRolesAsync(ApplicationUser user, IEnumerable<string> roles);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<IdentityResult> DeleteUsersAsync(IEnumerable<ApplicationUser> users);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);

    }
}
