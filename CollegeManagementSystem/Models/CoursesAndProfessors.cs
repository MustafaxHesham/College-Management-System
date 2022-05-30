namespace CollegeManagementSystem.Models
{
    public class CoursesAndProfessors
    {
        public int CourseId { get; set; }
        public Course? course { get; set; }
        public int ProfessorId { get; set; }
        public Professor? professor { get; set; }
    }
}
