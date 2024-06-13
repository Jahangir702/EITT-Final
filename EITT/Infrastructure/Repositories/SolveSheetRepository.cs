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
    /// Implementation of ISolveSheetRepository interface.
    /// </summary>
    public class SolveSheetRepository : Repository<SolveSheet>, ISolveSheetRepository
    {
        public SolveSheetRepository(EITTDbContext context) : base(context)
        {

        }

        /// <summary>
        /// The method is used to get a solvesheet by key.
        /// </summary>
        /// <param name="key">Primary key of the table SolveSheets</param>
        /// <returns>Returns a Issuelog if the key is matched.</returns>
        public async Task<SolveSheet> GetSolveSheetByKey(Guid key)
        {
            try
            {
                return await FirstOrDefaultAsync(d => d.Oid == key && d.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// The method is used to get the list of Solvesheets.
        /// </summary>
        /// <returns>Returns a list of all SolveSheets</returns>
        public async Task<IEnumerable<SolveSheet>> GetSolveSheets()
        {
            try
            {
                return await QueryAsync(s => s.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}