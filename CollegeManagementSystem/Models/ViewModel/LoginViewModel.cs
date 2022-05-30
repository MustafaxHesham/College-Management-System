using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models.ViewModel
{
    public class LoginViewModel
    {
        [EmailAddress,
        Required(ErrorMessage = "You must provide your email."),
        Display(Name = "Email")]
        public string? Email { get; set; }
        [DataType(DataType.Password),
        Required(ErrorMessage = "You must provide your password."),
        Display(Name = "Password")]
        public string? Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
