using Domain.Dto;
using Domain.Entities;
using Infrastructure.Contracts;

/*
 * Created by: Jahangir
 * Date created: 21.04.2024
 * Modified by: 
 * Last modified: 
 */
namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of IRecoveryRequestRepository interface.
    /// </summary>
    public class RecoveryRequestRepository : Repository<RecoveryRequest>, IRecoveryRequestRepository
    {
        public RecoveryRequestRepository(EITTDbContext context) : base(context)
        {

        }

        /// <summary>
        /// The method is used to get a recovery request by key.
        /// </summary>
        /// <param name="key">Primary key of the table RecoveryRequests</param>
        /// <returns>Returns a recovery request if the key is matched.</returns>
        public async Task<RecoveryRequest> GetRecoveryRequestByKey(Guid key)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.Oid == key && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The method is used to get a recovery request by date.
        /// </summary>
        /// <param name="recoveryRequestDate">Date of a recovery request.</param>
        /// <returns>Returns a recovery request if the date is matched.</returns>
        public async Task<IEnumerable<RecoveryRequest>> GetRecoveryRequestByDate(AdminRecoveryRequestDto recoveryRequestDate)
        {
            try
            {
                return await QueryAsync(c => c.IsDeleted == false && ((c.DateRequested) >= (recoveryRequestDate.DateFrom) && (c.DateRequested) <= (recoveryRequestDate.DateTo)) && c.IsRequestOpen == true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The method is used to get a recovery request by Username.
        /// </summary>
        /// <param name="Username">Username of a user.</param>
        /// <returns>Returns a recovery request if the Username is matched.</returns>
        public async Task<RecoveryRequest> GetRecoveryRequestByUsername(string Username)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.Username.Trim() == Username.Trim() && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The method is used to get the list of recovery requests.
        /// </summary>
        /// <returns>Returns a list of all recovery requests.</returns>
        public async Task<IEnumerable<RecoveryRequest>> GetRecoveryRequests()
        {
            try
            {
                return await QueryAsync(c => c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}