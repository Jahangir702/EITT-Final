using System.ComponentModel.DataAnnotations;

/*
 * Created by   : Jahangir
 * Date created : 16.04.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  : 
 * Reviewed Date:
 */
namespace Utilities.Constants
{
    /// <summary>
    /// Enums.
    /// </summary>
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// This enum is used to determine the type of the user.
        /// </summary>
        public enum UserType : byte
        {
            [Display(Name = "System Tester")]
            Tester = 1,

            [Display(Name = "System Developer")]
            Developer = 2
        }

        /// <summary>
        /// This enum is used Priority step of Issuelog
        /// </summary>
        public enum Priority : byte
        {
            High = 1,

            Standard = 2,

            Low = 3
        }

        /// <summary>
        /// This enum is used to check Solve Status.
        /// </summary>
        public enum Status : byte
        {
            Pending = 1,

            Resolved = 2,

            Failed = 3
        }
    }
}