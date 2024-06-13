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
    /// IssueLog Controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    //[Authorize]
    public class IssueLogController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public IssueLogController(IUnitOfWork context)
        {
            this.context = context;
        }

        /// <summary>
        /// eitt-api/issueLog
        /// </summary>
        /// <param name="issueLog"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(RouteConstants.CreateIssueLog)]
        public async Task<IActionResult> CreateIssueLog(IssueLog issueLog)
        {
            try
            {
                issueLog.DateCreated = DateTime.Now;
                issueLog.IsDeleted = false;

                context.IssueLogRepository.Add(issueLog);
                await context.SaveChangesAsync();

                return CreatedAtAction("ReadIssueLogByKey", new { key = issueLog.Oid }, issueLog);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/issueLogs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(RouteConstants.ReadIssueLogs)]
        public async Task<IActionResult> ReadIssueLogs()
        {
            try
            {
                var issueLogIndb = await context.IssueLogRepository.GetIssueLogs();
                return Ok(issueLogIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/issueLog/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table IssueLog</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadIssueLogByKey)]
        public async Task<IActionResult> ReadIssueLogByKey(Guid key)
        {
            try
            {
                if (key <= Guid.Empty)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var issueLogIndb = await context.IssueLogRepository.GetIssueLogByKey(key);

                if (issueLogIndb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
                return Ok(issueLogIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/issueLog/{key}
        /// </summary>
        /// <param name="key"></param>
        /// <param name="issueLog"></param>
        /// <returns>Http Status code: NoContent</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateissueLog)]
        public async Task<IActionResult> UpdateissueLog(Guid key, IssueLog issueLog)
        {
            try
            {
                var issueLogIndb = await context.IssueLogRepository.GetIssueLogByKey(issueLog.Oid);

                if (key != issueLog.Oid)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                issueLogIndb.DateModified = DateTime.Now;
                issueLogIndb.IsDeleted = false;

                context.IssueLogRepository.Update(issueLog);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/issueLog/{key}
        /// </summary>
        /// <param name="key">Primary key of the table IssueLog</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteissueLog)]
        public async Task<IActionResult> DeleteissueLog(Guid key)
        {
            try
            {
                if (key <= Guid.Empty)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var issueLogIndb = await context.IssueLogRepository.GetIssueLogByKey(key);

                if (issueLogIndb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                issueLogIndb.DateModified = DateTime.Now;
                issueLogIndb.IsDeleted = true;

                context.IssueLogRepository.Update(issueLogIndb);
                await context.SaveChangesAsync();

                return Ok(issueLogIndb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }
    }
}