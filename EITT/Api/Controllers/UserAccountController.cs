using Domain.Dto;
using Domain.Entities;
using Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Utilities.Constants;
using Utilities.Encryptions;

/*
 * Created by: Jahangir 
 * Date created: 18.04.2024
 * Modified by: 
 * Last modified: 
 */
namespace Api.Controllers
{
    /// <summary>
    /// UserAccount controller
    /// </summary>
    [Route(RouteConstants.BaseRoute)]
    [ApiController]
    //[Authorize]
    public class UserAccountController : ControllerBase
    {
        private UserAccount user;

        private readonly IUnitOfWork context;
        public UserAccountController(IUnitOfWork context)
        {
            user = new UserAccount();

            this.context = context;
        }

        /// <summary>
        /// eitt-api/user-account
        /// </summary>
        /// <param name="userAccount">UserAccountDto object</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpPost]
        [Route(RouteConstants.CreateUserAccount)]
        public async Task<ActionResult<UserAccount>> CreateUserAccount(UserAccount userAccount)
        {
            try
            {
                var userAccountWithSameUsername = await context.UserAccountRepository.GetUserAccountByUsername(userAccount.Username);

                if (userAccountWithSameUsername != null)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UsernameTaken);

                userAccount.DateCreated = DateTime.Now;
                userAccount.IsDeleted = false;

                EncryptionHelpers encryptionHelpers = new EncryptionHelpers();
                string encryptedPassword = encryptionHelpers.Encrypt(userAccount.Password);
                userAccount.Password = encryptedPassword;

                user = context.UserAccountRepository.Add(userAccount);
                await context.SaveChangesAsync();

                return Ok(userAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/user-account/user-check/{userName}
        /// </summary>
        /// <param name="userName">Username of a user</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.CheckUserName)]
        public async Task<IActionResult> GetUserAccountByUsername(string userName)
        {
            try
            {
                var userInDb = await context.UserAccountRepository.GetUserAccountByUsername(userName);

                return Ok(userInDb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/user-accounts
        /// </summary>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadUserAccounts)]
        public async Task<IActionResult> ReadUserAccounts()
        {
            try
            {
                var userAccount = await context.UserAccountRepository.GetUserAccounts();

                return Ok(userAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// eitt-api/user-account/key/{key}
        /// </summary>
        /// <param name="key">Primary key of the table UserAccounts.</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadUserAccountByKey)]
        public async Task<IActionResult> ReadUserAccountByKey(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var userAccount = await context.UserAccountRepository.GetUserAccountByKey(key);

                if (userAccount == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(userAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: eitt-api/user-account/Firstname/{Firstname}
        /// </summary>
        /// <param name="firstName">Firstname of a user.</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadUserAccountByFirstname)]
        public async Task<IActionResult> ReadUserAccountByFirstName(string firstName)
        {
            try
            {
                if (string.IsNullOrEmpty(firstName))
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var userAccount = await context.UserAccountRepository.GetUserAccountByFirstName(firstName);

                if (userAccount == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                return Ok(userAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: eitt-api/user-account/{key}
        /// </summary>
        /// <param name="key">Primary key of the table UserAccounts.</param>
        /// <param name="userAccount">UserAccount to be updated.</param>
        /// <returns>Http status code: NoContent.</returns>
        [HttpPut]
        [Route(RouteConstants.UpdateUserAccount)]
        public async Task<IActionResult> UpdateUserAccount(Guid key, UserAccountDto userAccount)
        {
            try
            {
                if (key != userAccount.Oid)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

                var userInDb = await context.UserAccountRepository.GetUserAccountByKey(userAccount.Oid);

                userInDb.FirstName = userAccount.FirstName;
                userInDb.Surname = userAccount.Surname;
                userInDb.Email = userAccount.Email;
                userInDb.DateModified = userAccount.DateModified;
                userInDb.ModifiedBy = userAccount.ModifiedBy;

                context.UserAccountRepository.Update(userInDb);
                await context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status204NoContent, MessageConstants.UpdateMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: eitt-api/user-account/{key}
        /// </summary>
        /// <param name="key">Primary key of the table UserAccounts.</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpDelete]
        [Route(RouteConstants.DeleteUserAccount)]
        public async Task<IActionResult> DeleteUserAccount(Guid key)
        {
            try
            {
                if (key == Guid.Empty)
                    return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

                var userAccountInDb = await context.UserAccountRepository.GetUserAccountByKey(key);

                if (userAccountInDb == null)
                    return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

                userAccountInDb.DateModified = DateTime.Now;
                userAccountInDb.IsDeleted = true;

                context.UserAccountRepository.Update(userAccountInDb);
                await context.SaveChangesAsync();

                return Ok(userAccountInDb);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }


        /// <summary>
        /// URL: eitt-api/user-account/login
        /// </summary>
        /// <param name="LoginDto">UserLogin of a user account.</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpPost]
        [Route(RouteConstants.UserLogin)]
        public async Task<IActionResult> UserLogin(LoginDto login)
        {
            try
            {
                EncryptionHelpers encryptionHelpers = new EncryptionHelpers();
                string encryptedPassword = encryptionHelpers.Encrypt(login.Password);

                var user = await context.UserAccountRepository.GetUserByUserNamePassword(login.Username, encryptedPassword);

                if (user != null)
                {
                    UserAccount userAccount = user;

                    return Ok(userAccount);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// URL: eitt-api/user-account/login
        /// </summary>
        /// <param name="LoginDto">UserLogin of a user account.</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpPost]
        [Route(RouteConstants.GetUserAccessByUserName)]
        public async Task<ActionResult<string>> GetUserAccessByUserName(string userName)
        {
            try
            {
                user = await context.UserAccountRepository.GetUserAccountByUsername(userName);

                if (user == null)
                    return BadRequest("User not found");

                EncryptionHelpers encryptionHelpers = new EncryptionHelpers();

                UserLoginSuccessDto userLoginSuccessDTO = new UserLoginSuccessDto() { UserAccount = user };

                return Ok(userLoginSuccessDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: eitt-api/user-account/user-access-by-username/{userName}
        /// </summary>
        /// <param name="userName">UserLogin of a user account.</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpGet]
        [Route(RouteConstants.ReadUserAccessByUserName)]
        public async Task<ActionResult<string>> ReadUserAccessByUserName(string userName)
        {
            try
            {
                user = await context.UserAccountRepository.GetUserAccountByUsername(userName);

                if (user == null)
                    return BadRequest("User not found");

                EncryptionHelpers encryptionHelpers = new EncryptionHelpers();

                UserLoginSuccessDto userLoginSuccessDTO = new UserLoginSuccessDto() { UserAccount = user };

                return Ok(userLoginSuccessDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: eitt-api/user-account/verify-password
        /// </summary>
        /// <param name="changePasswordDto">VerifyPassword of a user account.</param>
        /// <returns>Http status code: BadRequest.</returns>
        [HttpPost]
        [Route(RouteConstants.VerifyPassword)]
        public async Task<ActionResult<string>> VerifyPassword(VerifyPasswordDto changePasswordDto)
        {
            try
            {
                user = await context.UserAccountRepository.GetUserAccountByUsername(changePasswordDto.UserName);

                EncryptionHelpers encryptionHelpers = new EncryptionHelpers();

                string encryptedPassword = encryptionHelpers.Encrypt(changePasswordDto.Password);

                if (user.Password == encryptedPassword)
                    return Ok("true");

                return BadRequest(MessageConstants.WrongPasswordError);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: eitt-api/user-account/update-password
        /// </summary>
        /// <param name="changePasswordDto">UpdatePassword of a user account.</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpPost]
        [Route(RouteConstants.UpdatePassword)]
        public async Task<ActionResult<string>> UpdatePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                user = await context.UserAccountRepository.GetUserAccountByUsername(changePasswordDto.Username);

                EncryptionHelpers encryptionHelpers = new EncryptionHelpers();
                //string encryptedOldPassword = encryptionHelpers.Encrypt(changePasswordDto.Password);

                if (user.Password != changePasswordDto.Password)
                {
                    string encryptedOldPassword = encryptionHelpers.Encrypt(changePasswordDto.Password);

                    if (user.Password != encryptedOldPassword)
                        return BadRequest(MessageConstants.WrongPasswordError);
                }


                string encryptedPassword = encryptionHelpers.Encrypt(changePasswordDto.NewPassword);
                user.Password = encryptedPassword;

                context.UserAccountRepository.Update(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
            }
        }

        /// <summary>
        /// URL: eitt-api/user-account/recovery-request
        /// </summary>
        /// <param name="recoveryDto">RecoveryRequest of a user account.</param>
        /// <returns>Http status code: Ok.</returns>
        [HttpPost]
        [Route(RouteConstants.RecoveryRequest)]
        public async Task<IActionResult> RecoveryRequest([FromBody] RecoveryRequestDto recoveryDto)
        {
            try
            {
                var check = context.UserAccountRepository.GetbyUsername(recoveryDto.Username);

                UserAccount user = check;

                if (check != null)
                {
                    var userRecoveryTable = new RecoveryRequest()
                    {
                        Username = check.Username,
                        DateRequested = DateTime.Now,
                        IsRequestOpen = true
                    };

                    context.RecoveryRequestRepository.Add(userRecoveryTable);
                    await context.SaveChangesAsync();

                    return Ok(userRecoveryTable);
                }
                else
                {
                    return NotFound(MessageConstants.NoMatchFoundError);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}