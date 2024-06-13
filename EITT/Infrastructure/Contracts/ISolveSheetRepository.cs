using Domain.Entities;

/*
 * Created by   : Jahangir
 * Date created : 17.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date Reviewed:
 */
namespace Infrastructure.Contracts
{
    /// <summary>
    /// Implementation of ISolveSheetRepository interface
    /// </summary>
    public interface ISolveSheetRepository : IRepository<SolveSheet>
    {
        /// <summary>
        /// The method is used to get a SolveSheet by key.
        /// </summary>
        /// <param name="key">Primary key of SolveSheets</param>
        /// <returns>Returns a List of all SolveSheet</returns>
        public Task<SolveSheet> GetSolveSheetByKey(Guid key);

        /// <summary>
        /// The method is used to get the List of SolveSheet
        /// </summary>
        /// <param name="issueLogId">Primary key of IssueLog Table.</param>
        /// <returns>Returns a list of all SolveSheet</returns>
        public Task<IEnumerable<SolveSheet>> GetSolveSheets();
    }
}