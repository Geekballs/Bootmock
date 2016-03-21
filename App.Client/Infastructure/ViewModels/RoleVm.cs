using System.ComponentModel.DataAnnotations;

namespace App.Client.Infastructure.ViewModels
{
    public class RoleVm
    {
        [Display(Name = "Role ID")]
        public string Id { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "Required!", AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
