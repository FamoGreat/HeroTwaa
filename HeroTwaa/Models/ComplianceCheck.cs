namespace HeroTwaa.Models
{
    public class ComplianceCheck
    {
        public int Id { get; set; } // Primary Key
        public int ProjectId { get; set; } // Foreign Key
        public ComplianceType ComplianceType { get; set; } // e.g., Financial Regulation, Procurement Law
        public string Status { get; set; }
        public string Details { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Project Project { get; set; }
    }

    public enum ComplianceType
    {
        FinancialRegulation,
        ProcurementLaw
    }
}
