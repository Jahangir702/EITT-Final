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
    public class IssueLog : BaseModel
    {
        /// <summary>
        /// Primary Key of the table IssueLog.
        /// </summary>
        [Key]
        public Guid Oid { get; set; }

        /// <summary>
        /// IncidentCategory name of the IssueLog
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(60)]
        [MaxLength(60)]
        [DataType(DataType.Text)]
        [Display(Name = "IncidentCategory Name")]
        public string IncidentCategory { get; set; }

        /// <summary>
        /// IssueDescription of the IssueLog
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [DataType(DataType.Text)]
        [Display(Name = "Issue Description")]
        public string IssueDescription { get; set; }

        /// <summary>
        /// TestProdcedure of the IssueLog
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Test Prodcedure")]
        public string TestProdcedure { get; set; }

        /// <summary>
        /// Imagelinks of the IssueLog
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Image links")]
        public string Imagelinks { get; set; }

        /// <summary>
        /// Prototypelink of the IssueLog
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Prototype link")]
        public string Prototypelink { get; set; }

        /// <summary>
        /// Comments of the IssueLog
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Comments")]
        public string Comments { get; set; }

        /// <summary>
        /// Priority level of IssueLog
        /// </summary> 
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [Display(Name = "Priority")]
        public Priority Priority { get; set; }

        /// <summary>
        /// Date of IssueLog
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }

        [NotMapped]
        public bool EditMode { get; set; }

        [NotMapped]
        public bool ShowFullDescription { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the table UserAccount .
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        public Guid UserAccountId { get; set; }

        /// <summary>
        /// Navigation property.
        /// </summary>
        [ForeignKey("UserAccountId")]
        [JsonIgnore]
        public virtual UserAccount UserAccounts { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the table Module .
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        public int ModuleId { get; set; }

        /// <summary>
        /// Navigation property.
        /// </summary>
        [ForeignKey("ModuleId")]
        [JsonIgnore]
        public virtual Module Modules { get; set; }
        public virtual IEnumerable<SolveSheet> SolveSheets { get; set; }
    }
}