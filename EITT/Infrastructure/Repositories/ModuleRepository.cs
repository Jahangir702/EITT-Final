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
    /// Implementation of IModuleRepository interface.
    /// </summary>
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(EITTDbContext context) : base(context)
        {

        }


        public async Task<IEnumerable<Module>> GetModuleInfoByProject(int ProjectId)
        {
            try
            {
                var moduleInfos = await QueryAsync(m => m.IsDeleted == false && m.ProjectId == ProjectId, d => d.Projects);
                return moduleInfos.OrderBy(d => d.Description);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Module> GetModuleInfoByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(e => e.Oid == key && e.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Module> GetModuleInfoByName(string ModuleInfoName)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.Description.ToLower().Trim() == ModuleInfoName.ToLower().Trim() && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Module>> GetModulesInfos()
        {
            try
            {
                return await LoadListWithChildAsync<Module>(m => m.IsDeleted == false, d => d.Projects);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}