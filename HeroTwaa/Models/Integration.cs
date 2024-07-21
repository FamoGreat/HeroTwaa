namespace HeroTwaa.Models
{
    public class Integration
    {
        public int Id { get; set; } // Primary Key
        public string SystemName { get; set; }
        public string APIEndpoint { get; set; }
        public string AuthType { get; set; }
        public DateTime LastSyncDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
