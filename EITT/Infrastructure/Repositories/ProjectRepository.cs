using Domain.Entities;
using Infrastructure.Contracts;

/*
 * Created by   : Jahangir
 * Date created : 17-04-2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of IProjectRepository interface.
    /// </summary>
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(EITTDbContext context) : base(context)
        {

        }        

        /// <summary>
        /// The method is used to get a project by key
        /// </summary>
        /// <param name="key">Primary key of the table Projects</param>
        /// <returns>Returns a Project if the key is matched.</returns>
        public async Task<Project> GetProjectInfoByKey(int key)
        {
            try
            {
                return await FirstOrDefaultAsync(p => p.Oid == key && p.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// The method is used to get a Project by ProjectName.
        /// </summary>
        /// <param name="ProjectName">ProjectName of a project</param>
        /// <returns>Returns a Project if the project name is matched.</returns>
        public async Task<Project> GetProjectByName(string ProjectName)
        {
            try
            {
                return await FirstOrDefaultAsync(p => p.Description.ToLower().Trim() == ProjectName.ToLower().Trim() && p.IsDeleted == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// The method is used to get the list of Projects.
        /// </summary>
        /// <returns>Returns a list of all Projects</returns>
        public async Task<IEnumerable<Project>> GetProjects()
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