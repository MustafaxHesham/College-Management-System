using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "First Name"),
         Required(ErrorMessage = "First Name is required."),
         MinLength(1, ErrorMessage = "At least 2 char for First Name."),
         MaxLength(40, ErrorMessage = "At most 40 char for First Name.")
        ]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "You must provide your last name."),
        Display(Name = "Last Name")
        , MinLength(2, ErrorMessage = "Last name must exceed 2 chars."),
         MaxLength(40, ErrorMessage = "Last name must not exceed 40 chars.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "You must provide your city."),
        Display(Name = "City")
        , MinLength(1, ErrorMessage = "City name must exceed 1 chars."),
         MaxLength(40, ErrorMessage = "City name must not exceed 40 chars.")]
        public string? City { get; set; }

        [EmailAddress,
        Required(ErrorMessage = "You must provide your email."),
        Display(Name = "Email")]
        public string? Email { get; set; }
 
        [DataType(DataType.Password),
        Required(ErrorMessage = "You must provide your password."),
        Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password),
        Required(ErrorMessage = "You must provide your password confirmation."),
        Display(Name = "Confirm Password"),
        Compare("Password", ErrorMessage = "Passwords are not identical.")]
        public string? ConfirmPassword { get; set; }

    }
}
