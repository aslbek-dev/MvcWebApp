using System.ComponentModel.DataAnnotations;

namespace MvcWeb.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public DateTime LastLoginTime { get; set; } = DateTime.UtcNow;
        public DateTime RegistrationTime { get; set; } = DateTime.UtcNow;
    }
}
