using Domain.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.Constants;

/*
 * Created by   : Jahangir
 * Date created : 21.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Domain.Entities
{
    /// <summary>
    /// RecoveryRequest entity.
    /// </summary>
    public class RecoveryRequest : BaseModel
    {
        /// <summary>
        /// Primary key of the table RecoveryRequests.
        /// </summary>
        [Key]
        public Guid Oid { get; set; }

        /// <summary>
        /// Name of a user.
        /// </summary>
        [StringLength(20)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        // <summary>
        /// Date of recovery request of a user account.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date requested")]
        [IfFutureDate]
        public DateTime DateRequested { get; set; }

        /// <summary>
        /// Recovery request is open or not.
        /// </summary>
        [Display(Name = "Is request open")]
        public bool IsRequestOpen { get; set; }
    }
}