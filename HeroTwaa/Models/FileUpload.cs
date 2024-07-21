namespace HeroTwaa.Models
{
    public class FileUpload
    {
        public int Id { get; set; } // Primary Key
        public int ProjectId { get; set; } // Foreign Key
        public FileType FileType { get; set; } // e.g., Budget, Invoice, Contract, Plan, Blueprint
        public string FilePath { get; set; } // Path to the file storage location
        public int UploadedBy { get; set; } // User who uploaded the file
        public DateTime UploadedAt { get; set; } // Date and time of upload

        public Project Project { get; set; }
        public ApplicationUser User { get; set; } // User who uploaded the file
    }

    public enum FileType
    {
        Budget,
        Invoice,
        Contract,
        Plan,
        Blueprint
    }
}
