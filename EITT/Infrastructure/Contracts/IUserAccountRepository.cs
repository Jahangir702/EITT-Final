using Domain.Entities;

/*
 * Created by   : Jahangir
 * Date created : 17-04-2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Infrastructure.Contracts
{
    /// <summary>
    /// Implementation of IUserAccountRepository interface
    /// </summary>
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        /// <summary>
        /// The method is used to all user.
        /// </summary>
        /// <returns>Returns a user account list</returns>
        public Task <IEnumerable<UserAccount>> GetUserAccounts();

        /// <summary>
        /// The method is used to get a user account by username.
        /// </summary>
        /// <param name="username">Username of a user.</param>
        /// <returns>Returns a user account if the username is matched.
        public Task<UserAccount> GetUserAccountByUsername(string UserName);

        /// <summary>
        /// The method is used to get a user account by first name.
        /// </summary>
        /// <param name="firstName">First name of a user.</param>
        /// <returns>Returns a user account if the first name is matched.</returns>
        public Task<UserAccount> GetUserAccountByFirstName(string firstName);

        /// <summary>
        /// The method is used to get a user account by UserId.
        /// </summary>
        /// <param name="Username">Username of a user.</param>
        /// <returns>Returns a user account if the UserId is matched.</returns>
        public UserAccount GetbyUsername(string Username);

        /// <summary>
        /// The method is used to get a user account by key.
        /// </summary>
        /// <param name="key">Primary key of the table UserAccounts.</param>
        /// <returns>Returns a user account if the key is matched.</returns>
        public Task<UserAccount> GetUserAccountByKey(Guid key);

        /// <summary>
        /// The method is used to get a user account by username and password
        /// </summary>
        /// <param name="UserName">Username of a user account.</param>
        /// <param name="Password">Password of a user account</param>
        /// <returns>Returns a user account if the Username & password is matched.</returns>
        public Task<UserAccount> GetUserByUserNamePassword(string UserName, string Password);
    }
}