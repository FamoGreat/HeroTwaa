using Microsoft.AspNetCore.Identity;

namespace HeroTwaa.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public UserRole Role { get; set; } // e.g., Admin, Project Manager, Finance Officer
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public enum UserRole
    {
        Admin,
        ProjectManager,
        FinanceOfficer
    }
}
