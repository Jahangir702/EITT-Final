using Domain.Entities;
using Infrastructure.Contracts;

/*
 * Created by   : Jahangir
 * Date created : 18-04-2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of IIssueLogRepository interface.
    /// </summary>
    public class IssueLogRepository : Repository<IssueLog>, IIssueLogRepository
    {
        public IssueLogRepository(EITTDbContext context) : base(context)
        {

        }
        /// <summary>
        /// The method is used to get a IssueLog by key.
        /// </summary>
        /// <param name="key">Primary key of the table IssueLog</param>
        /// <returns>Returns a IssueLog if the key is matched.</returns>
        public async Task<IssueLog> GetIssueLogByKey(Guid key)
        {
            try
            {
                return await FirstOrDefaultAsync(i => i.Oid == key && i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// The method is used to get a IncidentCategoryName by Issuelog
        /// </summary>
        /// <param name="IncidentCategoryName">IncidentCategoryName of a IssueLog</param>
        /// <returns>Returns a IssueLog if the IncidentCategoryName is matched.</returns>
        public async Task<IssueLog> GetIssueLogByName(string IncidentCategoryName)
        {
            try
            {
                return await LoadWithChildAsync<IssueLog>(i => i.IncidentCategory.ToLower().Trim() == IncidentCategoryName.ToLower().Trim() && i.IsDeleted == false, m => m.Modules);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The method is used to get the list of IssueLogs.
        /// </summary>
        /// <returns>Returns a list of all IssueLog.</returns>
        public async Task<IEnumerable<IssueLog>> GetIssueLogs()
        {
            try
            {
                return await QueryAsync(p => p.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}