using System.ComponentModel.DataAnnotations;

namespace StoneCastle.WebSecurity.Models
{
    public class ChangePassword
    {
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,100})",  ErrorMessage = "Password must contain: minimum 6 characters, upper case letter and numberic value")]
        public string NewPassword { get; set; }

        public bool ChangePasswordSuccess { get; set; }

        public string UserName { get; set; }
    }
}
