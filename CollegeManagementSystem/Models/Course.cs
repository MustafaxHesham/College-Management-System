using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models
{
    [Authorize]
    public class Course
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "You should provide course description.")]
        [MinLength(2, ErrorMessage = "Name must not less than 2 chars.")]
        [MaxLength(40, ErrorMessage = "Name must not more than 40 chars.")]
        public string? Name { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "You should provide course description.")]
        [MinLength(2, ErrorMessage = "Code must not less than 2 chars.")]
        [MaxLength(7, ErrorMessage = "Code must not more than 7 chars.")]
        public string? Code { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "You should provide course description.")]
        [MinLength(4, ErrorMessage = "Description must not less than 4 chars.")]
        [MaxLength(40, ErrorMessage = "Description must not more than 40 chars.")]
        public string? Description { get; set; }

        [Display(Name = "Hours")]
        [Required(ErrorMessage="You should provide course hours.")]
        [Range(1, 5, ErrorMessage = "should not exceed 5 hours.")]
        public short hours { get; set; }

        [ValidateNever]
        public List<StudentsAndCourses> Students { get; set; }
        [ValidateNever]
        public List<CoursesAndProfessors> Professors { get; set; }
    }
}
