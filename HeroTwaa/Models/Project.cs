namespace HeroTwaa.Models
{
    public class Project
    {
        public int Id { get; set; } // Primary Key
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Budget { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<BillOfQuantities> Boqs { get; set; } // One-to-Many relationship with BillOfQuantities
    }
}
