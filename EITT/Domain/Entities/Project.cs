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
namespace Domain.Entities
{
    /// <summary>
    /// Project entity
    /// </summary>
    public class Project : BaseModel
    {
        /// <summary>
        /// Primary Key of the Project table 
        /// </summary>
        [Key]
        public int Oid { get; set; }

        /// <summary>
        /// Name of the Project
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(60)]
        [MaxLength(60), MinLength(2)]
        [DataType(DataType.Text)]
        [Display(Name = "Project Name")]
        public string Description { get; set; }

        [NotMapped]
        public bool EditMode { get; set; }
        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual IEnumerable<Module> Modules { get; set; }
    }
}