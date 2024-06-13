using Infrastructure.Contracts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

/*
 * Created by: Jahangir
 * Date created: 17.04.2024
 * Modified by: 
 * Last modified: 
 */
namespace Infrastructure
{
    /// <summary>
    /// Implementation of IUnitOfWork
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly EITTDbContext context;
        protected readonly IConfiguration configuration;
        private bool _disposed;

        public UnitOfWork(EITTDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        //Project
        #region ProjectRepository
        private IProjectRepository projectRepository;
        public IProjectRepository ProjectRepository
        {
            get
            {
                if (projectRepository == null)
                    projectRepository = new ProjectRepository(context);
                return projectRepository;
            }
        }
        #endregion

        //Module
        #region ModuleRepository
        private IModuleRepository moduleRepository;
        public IModuleRepository ModuleRepository
        {
            get
            {
                if (moduleRepository == null)
                    moduleRepository = new ModuleRepository(context);
                return moduleRepository;
            }
        }
        #endregion

        //IssueLog
        #region IssueLogRepository
        private IIssueLogRepository issueLogRepository;
        public IIssueLogRepository IssueLogRepository
        {
            get
            {
                if (issueLogRepository == null)
                    issueLogRepository = new IssueLogRepository(context);
                return issueLogRepository;
            }
        }
        #endregion

        //SolveSheet
        #region SolveSheetRepository
        private ISolveSheetRepository solveSheetRepository;
        public ISolveSheetRepository SolveSheetRepository
        {
            get
            {
                if(solveSheetRepository == null)
                    solveSheetRepository = new SolveSheetRepository(context);
                return solveSheetRepository;
            }
        }
        #endregion

        //UserAccount
        #region UserAccountRepository.
        private IUserAccountRepository userAccountRepository;
        public IUserAccountRepository UserAccountRepository
        {
            get
            {
                if (userAccountRepository == null)
                    userAccountRepository = new UserAccountRepository(context);

                return userAccountRepository;
            }
        }
        #endregion

        #region RecoveryRequestRepository
        private IRecoveryRequestRepository recoveryRequestRepository;
        public IRecoveryRequestRepository RecoveryRequestRepository
        {
            get
            {
                if (recoveryRequestRepository == null)
                    recoveryRequestRepository = new RecoveryRequestRepository(context);

                return recoveryRequestRepository;
            }
        }
        #endregion
        public IDbContextTransaction BeginTransaction()
        {
            return context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await context.Database.BeginTransactionAsync();
        }

        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}