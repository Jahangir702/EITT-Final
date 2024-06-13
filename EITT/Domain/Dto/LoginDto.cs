using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
    /// Login Dto.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// The row is assigned to the username of a user.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// The row is assigned to the password of a user account.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[JsonIgnore]
        //public UserAccount? userAccount { get; set; }

        public bool Validate()
        {
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            return Validator.TryValidateObject(this, context, results, true);
        }
    }
}