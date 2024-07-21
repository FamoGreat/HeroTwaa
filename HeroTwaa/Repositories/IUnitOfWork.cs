using HeroTwaa.Repositories.IRepository;

namespace HeroTwaa.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProjectRepository Projects { get; }
        IBillOfQuantitiesRepository BillOfQuantities { get; }
        IBoqItemRepository BoqItems { get; }
        IExpenseRepository Expenses { get; }
        IApprovalRepository Approvals { get; }
        IAuditTrailRepository AuditTrails { get; }
        INotificationRepository Notifications { get; }
        IReportRepository Reports { get; }
        IFileUploadRepository FileUploads { get; }
        IPublicDashboardRepository PublicDashboards { get; }
        IComplianceCheckRepository ComplianceChecks { get; }
        ITrainingModuleRepository TrainingModules { get; }
        IIntegrationRepository Integrations { get; }
        Task<int> CompleteAsync();
    }
}
