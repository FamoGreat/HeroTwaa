using HeroTwaa.Models;

namespace HeroTwaa.Services
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
