namespace HeroTwaa.Models
{
    public class BillOfQuantities
    {
        public int Id { get; set; } // Primary Key
        public int ProjectId { get; set; } // Foreign Key
        public string Description { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Project Project { get; set; }
        public ICollection<BoqItem> BoqItems { get; set; } // One-to-Many relationship with BoqItem
    }
}
