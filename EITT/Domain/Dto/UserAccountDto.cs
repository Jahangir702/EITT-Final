using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Utilities.Constants;

/*
 * Created by: Jahangir
 * Date created: 16.04.2024
 * Modified by: 
 * Last modified: 
 */
namespace Domain.Dto
{
    /// <summary>
    /// UserAccount dto.
    /// </summary>
    public class UserAccountDto : BaseModel
    {
        /// <summary>
        /// primary key Oid of the user.
        /// </summary> 
        public Guid Oid { get; set; }

        /// <summary>
        /// First name of the user.
        /// </summary>        
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(60)]
        [MaxLength(60), MinLength(2)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Surname of the user.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(60)]
        [MaxLength(60), MinLength(2)]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        /// <summary>
        /// E-mail address of the user. 
        /// </summary>
        [StringLength(20)]
        [MaxLength(20), MinLength(2)]
        [Display(Name = "E-mail address")]
        public string Email { get; set; }
    }
}