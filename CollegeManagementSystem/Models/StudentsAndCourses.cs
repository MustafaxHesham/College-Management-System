namespace CollegeManagementSystem.Models
{
    public class StudentsAndCourses
    {
        public int StudentId { get; set; }
        public Student student { get; set; }

        public int CourseId { get; set; }
        public Course course { get; set; }

        public int Grade { get; set; }
    }
}
