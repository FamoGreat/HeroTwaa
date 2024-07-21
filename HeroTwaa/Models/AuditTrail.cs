using System.ComponentModel.DataAnnotations;

namespace HeroTwaa.Models
{
    public class AuditTrail
    {
        public int Id { get; set; } // Primary Key
        public string ActionType { get; set; } // e.g., Created, Updated, Deleted
        public string Entity { get; set; } // e.g., User, Project, Expense
        public int EntityId { get; set; }
        public int UserId { get; set; } // Foreign Key
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }

        public ApplicationUser User { get; set; }
    }
}
