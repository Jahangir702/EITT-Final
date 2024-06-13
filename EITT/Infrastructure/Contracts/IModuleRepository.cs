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
    /// Implementation of IModuleRepository interface
    /// </summary>
    public interface IModuleRepository : IRepository<Module>
    {
        /// <summary>
        /// The method is used to get a ModuleInfo by ModuleInfo name.
        /// </summary>
        /// <param name="ModuleInfoName">Name of a ModuleInfo</param>
        /// <returns>Returns a Module if the Module name is matched.</returns>
        public Task<Module> GetModuleInfoByName(string ModuleInfoName);

        /// <summary>
        /// The method is used to get a ModuleInfo by key.
        /// </summary>
        /// <param name="key">Primary key of the table ModuleInfos</param>
        /// <returns>Returns a  ModuleInfos if the key is matched</returns>
        public Task<Module> GetModuleInfoByKey(int key);

        /// <summary>
        /// The method is used to get the ModuleInfo by ProjectId
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns>Returns a ModuleInfo if ProjectId is matched.</returns>
        public Task<IEnumerable<Module>> GetModuleInfoByProject(int ProjectId);

        /// <summary>
        /// The method is used to get the list of ModulesInfos.
        /// </summary>
        /// <returns>Returns a list of all ModulesInfo</returns>
        public Task<IEnumerable<Module>> GetModulesInfos();
    }
}