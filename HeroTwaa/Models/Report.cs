namespace HeroTwaa.Models
{
    public class Report
    {
        public int Id { get; set; } // Primary Key
        public int ProjectId { get; set; } // Foreign Key
        public int UserId { get; set; } // Foreign Key
        public ReportType ReportType { get; set; } // e.g., Budget vs. Actual, Cost Overrun Analysis
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }

        public Project Project { get; set; }
        public ApplicationUser User { get; set; }
    }
    public enum ReportType
    {
        BudgetVsActual,
        CostOverrunAnalysis
    }
}
