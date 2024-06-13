using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
    public class SolveSheet : BaseModel
    {
        /// <summary>
        /// Primary Key of the table SolveSheet.
        /// </summary>
        [Key]
        public Guid Oid { get; set; }

        /// <summary>
        /// Date of SolveSheet
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Solve Date")]
        public DateTime SolveDate { get; set; }

        /// <summary>
        /// Status level of Solvesheet
        /// </summary> 
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Display(Name = "Status")]
        public Status Status { get; set; }

        /// <summary>
        /// Comments of the SolveSheet
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [NotMapped]
        public bool EditMode { get; set; }

        /// <summary>
        /// Unique ResolvedId of the table SolveSheet.
        /// </summary>
        public Guid ResolvedId { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the table IssueLog .
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        public Guid IssueLogId { get; set; }

        /// <summary>
        /// Navigation property.
        /// </summary>
        [ForeignKey("IssueLogId")]
        [JsonIgnore]
        public virtual IssueLog IssueLogs { get; set; }
    }
}