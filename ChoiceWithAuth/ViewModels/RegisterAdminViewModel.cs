using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChoiceWithAuth.ViewModels
{
    public class RegisterAdminViewModel
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(6, ErrorMessage = "{0} must contain at least {1} characters.")]
        public string? Password { get; set; }

        [DisplayName("Confirm password")]
        [Compare("Password", ErrorMessage = "{0} must match with the {1}.")]
        public string? ConfirmPassword { get; set; }
    }
}
