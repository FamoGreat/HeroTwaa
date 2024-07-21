namespace HeroTwaa.Models
{
    public class PublicDashboard
    {
        public int Id { get; set; } // Primary Key
        public int ProjectId { get; set ; } // Foreign Key
        public string Title { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Project Project { get; set; }
    }
}
