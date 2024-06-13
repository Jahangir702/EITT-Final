using Microsoft.EntityFrameworkCore.Storage;

/*
 * Created by   : Jahangir
 * Date created : 17.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Infrastructure.Contracts
{
    /// <summary>
    /// IUnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        IProjectRepository ProjectRepository { get; }
        IModuleRepository ModuleRepository { get; }
        IIssueLogRepository IssueLogRepository { get; }
        ISolveSheetRepository SolveSheetRepository { get; }

        //USER MODULE
        IUserAccountRepository UserAccountRepository { get; }
        IRecoveryRequestRepository RecoveryRequestRepository { get; }
    }
}
