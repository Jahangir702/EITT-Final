using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Authorization;
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
    /// SolveSheet Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    //[Authorize]
    public class SolveSheetController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public SolveSheetController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// eitt-api/solvesheet
        /// </summary>
        /// <param name="solveSheet"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(RouteConstants.CreateSolveSheet)]
        public async Task<IActionResult> CreateSolveSheet(SolveSheet solveSheet)
        {
            try
            {
                solveSheet.DateCreated = DateTime.Now;
                solveSheet.IsDeleted = false;

                context.SolveSheetRepository.Add(solveSheet);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadSolveSheetByKey", new {key = solveSheet.Oid}, solveSheet);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/solvesheets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(RouteConstants.ReadSolveSheets)]
        public async Task<IActionResult> ReadSolveSheets()
        {
            try
            {
                var solvesheetIndb = await context.SolveSheetRepository.GetSolveSheets();
                return Ok(solvesheetIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/solvesheet/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table SolveSheet</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadSolveSheetByKey)]
        public async Task<IActionResult> ReadSolveSheetByKey(Guid key)
        {
            try
            {
                if (key <= Guid.Empty)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var solvesheetIndb = await context.SolveSheetRepository.GetSolveSheetByKey(key);

                if (solvesheetIndb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                return Ok(solvesheetIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/solvesheet/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="solveSheet"></param>
        /// <returns>Http Status code: NoContent</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateSolveSheet)]
        public async Task<IActionResult> UpdateSolveSheet(Guid key, SolveSheet solveSheet)
        {
            try
            {
                var solvesheetIndb = await context.SolveSheetRepository.GetSolveSheetByKey(solveSheet.Oid);

                if (key != solveSheet.Oid)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                solvesheetIndb.DateCreated = DateTime.Now;
                solvesheetIndb.IsDeleted = false;

                context.SolveSheetRepository.Update(solveSheet);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/solvesheet/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Http status code: Ok.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteSolveSheet)]
        public async Task<IActionResult> DeleteSolveSheet(Guid key)
        {
            try
            {
                if(key <= Guid.Empty)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var solvesheetIndb = await context.SolveSheetRepository.GetSolveSheetByKey(key);

                if (solvesheetIndb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                solvesheetIndb.DateModified = DateTime.Now;
                solvesheetIndb.IsDeleted = true;

                context.SolveSheetRepository.Update(solvesheetIndb);
                await context.SaveChangesAsync();

                return Ok(solvesheetIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}