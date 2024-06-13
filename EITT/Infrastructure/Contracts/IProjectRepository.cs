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
    /// Implementation of IProjectRepository interface
    /// </summary>
    public interface IProjectRepository : IRepository<Project>
    {
        /// <summary>
        /// The method is used to get a ProjectInfo by Key
        /// </summary>
        /// <param name="key">Primary key of the table ProjectInfo</param>
        /// <returns>Returns a ProjectInfo if the Project name is matched</returns>
        public Task<Project> GetProjectInfoByKey(int key);

        /// <summary>
        /// The method is used to get a Project by Project name.
        /// </summary>
        /// <param name="ProjectName">Name of a Project.</param>
        /// <returns>Returns a Project if the Project name is matched.</returns>
        public Task<Project> GetProjectByName(string ProjectName);

        /// <summary>
        /// Returns all Project
        /// </summary>
        /// <returns>List of Project Object</returns>
        public Task<IEnumerable<Project>> GetProjects();
    }
}