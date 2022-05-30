using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        [Display(Name = "Social Security Number")]
        [Required(ErrorMessage = "You should provide your Social Security Number.")]
        [MinLength(4, ErrorMessage = "SSN must not less than 4 chars.")]
        [MaxLength(20, ErrorMessage = "SSN must not more than 20 chars.")]
        public string? SSN { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "You should provide your name.")]
        [MinLength(4, ErrorMessage = "Name must not less than 5 chars.")]
        [MaxLength(50, ErrorMessage = "Name must not more than 50 chars.")]
        public string? Name { get; set; }
        [Display(Name = "Age")]
        [Required(ErrorMessage = "You should provide your age.")]
        [Range(15, int.MaxValue, ErrorMessage = "Age should not be less than 15 years")]
        public int Age { get; set; }

        [ValidateNever]
        public string? imageUrl { get; set; }
    }
}
