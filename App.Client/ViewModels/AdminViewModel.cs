using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace App.Client.ViewModels
{
    public class AdminVm
    {

        public class CreateUser
        {
            [Display(Name = "Email")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Password")]
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Confirm password")]
            [DataType(DataType.Password)]
            [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class EditUser
        {
            public string Id { get; set; }

            [Display(Name = "Username")]
            //[Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [EmailAddress]
            public string UserName { get; set; }

            [Display(Name = "Email")]
            //[Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Email Confirmed")]
            public bool EmailConfirmed { get; set; }

            public IEnumerable<SelectListItem> RolesList { get; set; }
        }

        public class ResetUserPassword
        {
            public string Id { get; set; }

            [Display(Name = "Username")]
            public string Username { get; set; }

            [Display(Name = "New Password")]
            [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }
        }
    }
}