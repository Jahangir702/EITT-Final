/*
 * Created by: Jahangir
 * Date created: 21.04.2024
 * Modified by: 
 * Last modified: 
 */
namespace Domain.Dto
{
    /// <summary>
    /// Verify Password Dto.
    /// </summary>
    public class VerifyPasswordDto
    {
        /// <summary>
        /// The row is assigned to the password of a user account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The row is assigned to the username of a user.
        /// </summary>
        public string UserName { get; set; }
    }
}