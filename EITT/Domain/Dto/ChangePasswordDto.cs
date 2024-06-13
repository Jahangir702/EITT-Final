﻿using System.ComponentModel.DataAnnotations;
using Utilities.Constants;

/*
 * Created by   : Jahangir 
 * Date created : 21.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Domain.Dto
{
    /// <summary>
    /// ChangePassword Dto.
    /// </summary>
    public class ChangePasswordDto
    {
        /// <summary>
        /// The row is assigned to the username of a user.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(30)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// The row is assigned to the password of a user account.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DataType(DataType.Password)]
        [Display(Name = "Login password")]
        public string Password { get; set; }

        /// <summary>
        /// The row is assigned to the new password of a user account.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DataType(DataType.Password)]
        [Display(Name = "Login password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// The row is assigned to the confirm password of a user account.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = MessageConstants.PasswordMatchError)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}