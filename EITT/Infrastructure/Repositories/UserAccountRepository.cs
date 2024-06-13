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
    /// Implementation of IUserAccountRepository interface.
    /// </summary>
    public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(EITTDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<UserAccount>> GetUserAccounts()
        {
            try
            {
                return await QueryAsync(u => u.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserAccount> GetUserAccountByKey(Guid key)
        {
            try
            {
                return await FirstOrDefaultAsync(u => u.Oid == key && u.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserAccount> GetUserAccountByUsername(string UserName)
        {
            try
            {
                var user = new UserAccount();
                return await FirstOrDefaultAsync(u =>u.Username.ToLower().Trim() == UserName.ToLower().Trim() && u.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserAccount> GetUserAccountByFirstName(string firstName)
        {
            try
            {
                return await FirstOrDefaultAsync(u => u.FirstName.ToLower().Trim() == firstName.ToLower().Trim() && u.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserAccount GetbyUsername(string Username)
        {
            try
            {
                if (Username != null)
                {
                    var result = context.UserAccounts.FirstOrDefault(x => x.Username == Username);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserAccount> GetUserByUserNamePassword(string UserName, string Password)
        {
            try
            {
                var results = await FirstOrDefaultAsync(u => u.Username == UserName && u.Password == Password);

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}