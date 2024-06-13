using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Utilities.Constants;

/*
 * Created by   : Jahangir
 * Date created : 16.04.2024
 * Modified by  :
 * Last modified:
 * Reviewed by  :
 * Date reviewed:
 */
namespace Domain.Entities
{
    /// <summary>
    /// Module Entity
    /// </summary>
    public class Module : BaseModel
    {
        /// <summary>
        /// Primary key of the Module table
        /// </summary>
        [Key]
        public int Oid { get; set; }

        /// <summary>
        /// Name of the Module
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(60)]
        [MaxLength(60), MinLength(2)]
        [DataType(DataType.Text)]
        [Display(Name = "Module Name")]
        public string Description { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the table Project .
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        public int ProjectId { get; set; }

        /// <summary>
        /// Navigation property.
        /// </summary>
        [ForeignKey("ProjectId")]
        [JsonIgnore]
        public virtual Project Projects { get; set; }

        public virtual IEnumerable<IssueLog> IssueLogs { get; set; }
    }
}