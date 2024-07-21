using HeroTwaa.Data;
using HeroTwaa.Models;
using HeroTwaa.Repositories.IRepository;
using HeroTwaa.Repositories.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HeroTwaa.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            dbContext = context;
            Users = new UserRepository(dbContext, userManager, signInManager, roleManager);
            Projects = new ProjectRepository(dbContext);
            BillOfQuantities = new BillOfQuantitiesRepository(dbContext);
            BoqItems = new BoqItemRepository(dbContext);
            Expenses = new ExpenseRepository(dbContext);
            Approvals = new ApprovalRepository(dbContext);
            AuditTrails = new AuditTrailRepository(dbContext);
            Notifications = new NotificationRepository(dbContext);
            Reports = new ReportRepository(dbContext);
            FileUploads = new FileUploadRepository(dbContext);
            PublicDashboards = new PublicDashboardRepository(dbContext);
            ComplianceChecks = new ComplianceCheckRepository(dbContext);
            TrainingModules = new TrainingModuleRepository(dbContext);
            Integrations = new IntegrationRepository(dbContext);
        }

        public IUserRepository Users { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IBillOfQuantitiesRepository BillOfQuantities { get; private set; }
        public IBoqItemRepository BoqItems { get; private set; }
        public IExpenseRepository Expenses { get; private set; }
        public IApprovalRepository Approvals { get; private set; }
        public IAuditTrailRepository AuditTrails { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IReportRepository Reports { get; private set; }
        public IFileUploadRepository FileUploads { get; private set; }
        public IPublicDashboardRepository PublicDashboards { get; private set; }
        public IComplianceCheckRepository ComplianceChecks { get; private set; }
        public ITrainingModuleRepository TrainingModules { get; private set; }
        public IIntegrationRepository Integrations { get; private set; }
        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
