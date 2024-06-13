using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;

/*
 * Created by: Jahangir 
 * Date created: 18.04.2024
 * Modified by: 
 * Last modified: 
 */
namespace Api.Controllers
{
    /// <summary>
    /// Project Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    //[Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public ProjectController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// Checks wheather the Project name is duplicate or not.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(RouteConstants.CreateProject)]
        public async Task<IActionResult> CreateProject(Project project)
        {
            try
            {
                if (await IsProjectDuplicate(project) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                project.DateCreated = DateTime.Now;
                project.IsDeleted = false;

                context.ProjectRepository.Add(project);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadProjectByKey", new { key = project.Oid }, project);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// checks whether the project name is duplicate or not.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private async Task<bool> IsProjectDuplicate(Project project)
        {
            try
            {
                var projectInDb = await context.ProjectRepository.GetProjectByName(project.Description);

                if (projectInDb != null)
                    if (projectInDb.Oid != project.Oid)
                        return true;
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// URL: eitt-api/projects
        /// </summary>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadProjects)]
        public async Task<IActionResult> ReadProjects()
        {
            try
            {
                var projectInDb = await context.ProjectRepository.GetProjects();

                return Ok(projectInDb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/project/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table projects</param>
        /// <returns> Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadProjectByKey)]
        public async Task<IActionResult> ReadProjectByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var projectInDb = await context.ProjectRepository.GetProjectInfoByKey(key);

                if (projectInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(projectInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/project/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="project"></param>
        /// <returns> Http status code: Ok.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateProject)]
        public async Task<IActionResult> UpdateProject(int key, Project project)
        {
            try
            {
                if (key != project.Oid)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                if (await IsProjectDuplicate(project) == true)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateError);

                project.DateModified = DateTime.Now;
                project.IsDeleted = false;

                context.ProjectRepository.Update(project);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/project/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <returns> Http status code: Ok.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteProject)]
        public async Task<IActionResult> DeleteProject(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var projectInDb = await context.ProjectRepository.GetProjectInfoByKey(key);

                if (projectInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                projectInDb.DateModified = DateTime.Now;
                projectInDb.IsDeleted = true;

                context.ProjectRepository.Update(projectInDb);
                await context.SaveChangesAsync();

                return Ok(projectInDb);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}