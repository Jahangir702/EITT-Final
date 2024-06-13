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
    /// Implementation of IIssueRepository interface
    /// </summary>
    public interface IIssueLogRepository : IRepository<IssueLog>
    {
        /// <summary>
        /// The method is used to get a IssueLog by IncidentCategoryName
        /// </summary>
        /// <param name="IncidentCategoryName">IncidentCategoryName of a IssueLog</param>
        /// <returns>Returns a IssueLog if the IncidentCategoryName is matched</returns>
        public Task<IssueLog> GetIssueLogByName(string IncidentCategoryName);

        /// <summary>
        /// The method is used to get a Issuelog by key. 
        /// </summary>
        /// <param name="key">Primary key of the table IssueLogs</param>
        /// <returns>Returns a Issuelog  if the key is matched.</returns>
        public Task<IssueLog> GetIssueLogByKey(Guid key);

        /// <summary>
        /// Returns all IssueLog
        /// </summary>
        /// <returns>List of IssueLog Object</returns>
        public Task<IEnumerable<IssueLog>> GetIssueLogs();
    }
}