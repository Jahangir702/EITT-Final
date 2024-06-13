using System.ComponentModel.DataAnnotations;

/*
 * Created by: Jahangir
 * Date created: 21.04.2024
 * Modified by: 
 * Last modified: 
 */
namespace Domain.Dto
{
    public class RecoveryRequestDto
    {
        /// <summary>
        /// Primary key of the table RecoveryRequests.
        /// </summary>
        [Key]
        public Guid Oid { get; set; }

        /// <summary>
        /// The row is assigned to the username of a user.
        /// </summary>
        /// 
        //[Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(30)]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}