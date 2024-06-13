using Domain.Entities;

/*
 * Created by: Jahangir
 * Date created: 21.04.2024
 * Modified by: 
 * Last modified: 
 */
namespace Domain.Dto
{
    /// <summary>
    /// UserLoginSuccess Dto.
    /// </summary>
    public class UserLoginSuccessDto
    {
        /// <summary>
        /// The row is assigned to the user account of a user.
        /// </summary>
        public UserAccount UserAccount { get; set; }
    }
}