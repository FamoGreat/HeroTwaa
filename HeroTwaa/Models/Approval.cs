namespace HeroTwaa.Models
{
    public class Approval
    {
        public int Id { get; set; } // Primary Key
        public int ExpenseId { get; set; } // Foreign Key
        public int ProjectId { get; set; } // Foreign Key
        public int UserId { get; set; } // Foreign Key
        public ApprovalStatus Status { get; set; } // e.g., Pending, Approved, Rejected
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Expense Expense { get; set; }
        public Project Project { get; set; }
        public ApplicationUser User { get; set; }
    }

    public enum ApprovalStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
