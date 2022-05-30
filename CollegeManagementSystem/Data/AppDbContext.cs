using CollegeManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CoursesAndProfessors>().HasKey("ProfessorId", "CourseId");
            builder.Entity<StudentsAndCourses>().HasKey("CourseId", "StudentId");
            builder.Entity<CoursesAndProfessors>().HasOne(c => c.professor).WithMany(p => p.Courses).HasForeignKey(pc => pc.ProfessorId);

            builder.Entity<CoursesAndProfessors>().HasOne(pc => pc.course).WithMany(c => c.Professors).HasForeignKey(pc => pc.CourseId);

            builder.Entity<StudentsAndCourses>().HasOne(sc => sc.student).WithMany(s => s.Courses).HasForeignKey(sc => sc.StudentId);

            builder.Entity<StudentsAndCourses>().HasOne(sc => sc.course).WithMany(c => c.Students).HasForeignKey(sc => sc.CourseId);

            base.OnModelCreating(builder);
        }



        public DbSet<User>? users { set; get; }
        public DbSet<Course>? courses { set; get; }
        public DbSet<Professor>? professors { set; get; }
        public DbSet<Student>? students { set; get; }
        public DbSet<CoursesAndProfessors>? coursesAndProfessors { set; get; }
        public DbSet<StudentsAndCourses> StudentsAndCourses { set; get; }
    }
}
