using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models
{
    public class Professor : Person
    {
        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be positive number.")]
        [Display(Name = "Salary")]
        public double Salary { get; set; }

        [Display(Name = "Specialization")]
        [Required(ErrorMessage = "You should provide specialization.")]
        [MinLength(1, ErrorMessage = "Specialization must be more than 1 char.")]
        [MaxLength(20, ErrorMessage = "Specialization must be less than 20 chars.")]
        public string? Specialization { get; set; }


        [ValidateNever]
        public List<CoursesAndProfessors>? Courses { get; set; }
    }
}
