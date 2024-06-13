using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.Constants;
using static Utilities.Constants.Enums;

/*
 * Created by   : Jahangir
 * Date created : 16.04.2020
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Domain.Entities
{
    /// <summary>
    /// UserAccount entity.
    /// </summary>
    public class UserAccount : BaseModel
    {
        /// <summary>
        /// Primary Key of the table UserAccounts.
        /// </summary>
        [Key]
        public Guid Oid { get; set; }

        /// <summary>
        /// First name of the user.
        /// </summary>        
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(20)]
        [MaxLength(20), MinLength(2)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Surname of the user.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(20)]
        [MaxLength(20), MinLength(2)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        /// <summary>
        /// E-mail address of the user. 
        /// </summary>
        [StringLength(20)]
        [MaxLength(20), MinLength(2)]
        [Display(Name = "E-mail address")]
        public string Email { get; set; }

        /// <summary>
        /// Type of user of a user account.
        /// </summary> 
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Display(Name = "User type")]
        public UserType UserType { get; set; }

        /// <summary>
        /// Username of the user.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(30)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Password of the user.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.PasswordLengthError)]
        [MaxLength(90), MinLength(8)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Password confirmation of the user.
        /// </summary>
        [NotMapped]
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Compare("Password", ErrorMessage = MessageConstants.PasswordMatchError)]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Navigation property.
        /// </summary>
        public virtual IEnumerable<IssueLog> IssueLogs { get; set; }
    }
}