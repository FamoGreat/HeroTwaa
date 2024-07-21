namespace HeroTwaa.Models
{
    public class Notification
    {
        public int Id { get; set; } // Primary Key
        public int UserId { get; set; } // Foreign Key
        public string Message { get; set; }
        public NotificationType Type { get; set; } // e.g., Budget Alert, Approval Request
        public NotificationStatus Status { get; set; } // e.g., Unread, Read
        public DateTime CreatedAt { get; set; }

        public ApplicationUser User { get; set; }
    }

    public enum NotificationType
    {
        BudgetAlert,
        ApprovalRequest
    }
    public enum NotificationStatus
    {
        Unread,
        Read
    }
}
