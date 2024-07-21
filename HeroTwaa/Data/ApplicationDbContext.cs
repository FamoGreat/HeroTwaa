using HeroTwaa.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<BillOfQuantities> BillOfQuantities { get; set; }
        public DbSet<BoqItem> BoqItems { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Approval> Approvals { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<PublicDashboard> PublicDashboards { get; set; }
        public DbSet<ComplianceCheck> ComplianceChecks { get; set; }
        public DbSet<TrainingModule> TrainingModules { get; set; }
        public DbSet<Integration> Integrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Expense>().Property(e => e.Category)
                .HasConversion<string>();

            modelBuilder.Entity<Approval>().Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<FileUpload>().Property(f => f.FileType)
                .HasConversion<string>();

            modelBuilder.Entity<ComplianceCheck>().Property(c => c.ComplianceType)
                .HasConversion<string>();

            modelBuilder.Entity<Notification>().Property(n => n.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Notification>().Property(n => n.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Report>().Property(r => r.ReportType)
                .HasConversion<string>();

            modelBuilder.Entity<BoqItem>().Property(b => b.Unit)
                .HasConversion<string>();

            // Specify OnDelete behavior to avoid cycles or multiple cascade paths
            modelBuilder.Entity<Approval>()
                .HasOne(a => a.Expense)
                .WithMany(e => e.Approvals)
                .HasForeignKey(a => a.ExpenseId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Approval>()
            //    .HasOne(a => a.Project)
            //    .WithMany(p => p.Approvals)
            //    .HasForeignKey(a => a.ProjectId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Approval>()
            //    .HasOne(a => a.User)
            //    .WithMany(u => u.Approvals)
            //    .HasForeignKey(a => a.UserId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
