namespace HeroTwaa.Models
{
    public class TrainingModule
    {
        public int Id { get; set; } // Primary Key
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; } // Can be text, links to videos, documents, etc.
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
