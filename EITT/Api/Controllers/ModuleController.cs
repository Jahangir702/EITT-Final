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
    /// Module Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    //[Authorize]
    public class ModuleController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public ModuleController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// eitt-api/module
        /// </summary>
        /// <param name="module"></param>
        /// <returns>Http status code: CreateAT.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateModule)]
        public async Task<IActionResult> CreateModule(Module module)
        {
            try
            {
                var existingModule = await context.ModuleRepository.GetModuleInfoByName(module.Description);
                if (existingModule != null && existingModule.ProjectId == module.ProjectId)
                {
                    return Conflict(new { message = "A module with the same name already exists in the selected project." });
                }

                module.DateCreated = DateTime.Now;
                module.IsDeleted = false;

                context.ModuleRepository.Add(module);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadModuleByKey", new { key = module.Oid }, module);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/modules
        /// </summary>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadModules)]
        public async Task<IActionResult> ReadModules()
        {
            try
            {
                var moduleIndb = await context.ModuleRepository.GetModulesInfos();
                return Ok(moduleIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/module/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table Module</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadModuleByKey)]
        public async Task<IActionResult> ReadModuleByKey(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var moduleIndb = await context.ModuleRepository.GetModuleInfoByKey(key);

                if (moduleIndb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                return Ok(moduleIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }


        /// <summary>
        /// eitt-api/module/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="module"></param>
        /// <returns>Http Status code: NoContent</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateModule)]
        public async Task<IActionResult> UpdateModule(int key, Module module)
        {
            try
            {
                var moduleIndb = await context.ModuleRepository.GetModuleInfoByKey(module.Oid);

                if (key != module.Oid)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                // Check for duplicate module name within the same project
                var existingModule = await context.ModuleRepository.GetModuleInfoByName(module.Description);
                if (existingModule != null && existingModule.ProjectId == moduleIndb.ProjectId && existingModule.Oid != moduleIndb.Oid)
                {
                    return Conflict(new { message = "A module with the same name already exists in the selected project." });
                }

                moduleIndb.DateModified = DateTime.Now;
                moduleIndb.IsDeleted = false;

                context.ModuleRepository.Update(module);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/module/{key}
        /// </summary>
        /// <param name="key">Primary Key of the table module</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteModule)]
        public async Task<IActionResult> DeleteModule(int key)
        {
            try
            {
                if (key <= 0)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var moduleIndb = await context.ModuleRepository.GetModuleInfoByKey(key);

                if (moduleIndb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                moduleIndb.DateModified = DateTime.Now;
                moduleIndb.IsDeleted = true;

                context.ModuleRepository.Update(moduleIndb);
                await context.SaveChangesAsync();

                return Ok(moduleIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

    }
}