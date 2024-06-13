using Domain.Dto;
using Domain.Entities;

/*
 * Created by: Jahangir
 * Date created: 21.04.2024
 * Modified by: 
 * Last modified: 
 */
namespace Infrastructure.Contracts
{
    /// <summary>
    /// Implementation of IRecoveryRequestRepository interface
    /// </summary>
    public interface IRecoveryRequestRepository : IRepository<RecoveryRequest>
    {
        /// <summary>
        /// The method is used to get a recovery request by key.
        /// </summary>
        /// <param name="key">Primary key of the table RecoveryRequests</param>
        /// <returns>Returns a recovery request if the key is matched.</returns>
        public Task<RecoveryRequest> GetRecoveryRequestByKey(Guid key);

        /// <summary>
        /// The method is used to get a recovery request by date.
        /// </summary>
        /// <param name="recoveryRequestDate">Date of a recovery request.</param>
        /// <returns>Returns a recovery request if the date is matched.</returns>
        public Task<IEnumerable<RecoveryRequest>> GetRecoveryRequestByDate(AdminRecoveryRequestDto recoveryRequestDate);

        /// <summary>
        /// The method is used to get a recovery request by Username.
        /// </summary>
        /// <param name="Username">Username of a user.</param>
        /// <returns>Returns a recovery request if the Username is matched.</returns>
        public Task<RecoveryRequest> GetRecoveryRequestByUsername(string Username);

        /// <summary>
        /// The method is used to get the list of recovery requests.
        /// </summary>
        /// <returns>Returns a list of all recovery requests.</returns>
        public Task<IEnumerable<RecoveryRequest>> GetRecoveryRequests();
    }
}
