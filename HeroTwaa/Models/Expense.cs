namespace HeroTwaa.Models
{
    public class Expense
    {
        public int Id { get; set; } // Primary Key
        public int ProjectId { get; set; } // Foreign Key
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; } // e.g., Material, Tools, Labor
        public bool Approved { get; set; }
        public int? BoqItemId { get; set; } // Foreign Key (nullable)
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Project Project { get; set; }
        public BoqItem BoqItem { get; set; } // Linking to BOQ item
        public ICollection<Approval> Approvals { get; set; } // One-to-Many relationship with Approval
    }

    public enum ExpenseCategory
    {
        Material,
        Tools,
        Labor
    }
}
