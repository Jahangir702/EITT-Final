using System.ComponentModel.DataAnnotations;
using Utilities.Constants;

/*
 * Created by   : Jahangir 
 * Date created : 21.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace Domain.Validators
{
    public class IfFutureDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;

                if (date.Date > DateTime.Now.Date)
                    return new ValidationResult(MessageConstants.IfFutureDate);
            }

            return ValidationResult.Success;
        }
    }
}