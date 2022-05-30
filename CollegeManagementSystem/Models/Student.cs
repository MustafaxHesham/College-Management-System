using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models
{
    public class Student : Person
    {
        public int TotalPassedHours { get; set; }
        [Range(0, 100, ErrorMessage = "Grade must be between 0 & 100")]
        public double GPA { get; set; } = 0;
        public int Level { get; set; } = 1;

        [ValidateNever]
        public List<StudentsAndCourses> Courses { get; set; }
    }
}
