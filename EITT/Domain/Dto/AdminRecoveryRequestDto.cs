using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.Constants;

/*
 * Created by   : Jahangir
 * Date created : 16.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Domain.Dto
{
    /// <summary>
    /// AdminRecoveryRequest Dto.
    /// </summary>
    public class AdminRecoveryRequestDto
    {
        /// <summary>
        /// The row is assigned to the 'date from' of a recovery request.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date from")]
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// The row is assigned to the 'data to' of a recovery request.
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date to")]
        public DateTime DateTo { get; set; }
    }
}