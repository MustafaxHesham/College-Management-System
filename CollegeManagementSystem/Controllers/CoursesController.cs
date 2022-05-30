using CollegeManagementSystem.Data;
using CollegeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeManagementSystem.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly AppDbContext context;

        public CoursesController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.courses);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description,Code,hours")] Course course)
        {
            if (ModelState.IsValid)
            {
                context.courses?.Add(course);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(course);
        }

        public IActionResult Details(int id)
        {
            Course? course = context.courses?.Include(c => c.Professors).ThenInclude(p => p.professor).FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                ViewBag.NotFound = "Course Not Found :(";
                return View();
            }
            else
                return View(course);
        }

        [HttpGet]
        public IActionResult Delete(int id) => View(context.courses?.Find(id));

        [HttpPost]
        public IActionResult Delete(Course course, int id)
        {
            if (course.Id != id)
                return View();
            if (course == null)
            {
                ViewBag.NotFound = "Course Not Found :(";
                return View();
            }
            else
            {
                context.courses?.Remove(course);
                context.SaveChanges();
                return RedirectToAction("Index", "Courses");
            }
        }

        [HttpGet]
        public IActionResult Update(int id) => View(context.courses?.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([Bind("Id,Name,Description,Code,hours")] Course course, int id) {
            if (course.Id != id)
                return View(-1);
            else
            {
                if (ModelState.IsValid)
                {
                    context.courses?.Update(course);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Courses");
                }
                else
                    return View(course);
            }
        }
        [HttpGet]
        public IActionResult AssignProfessor(int id)
        {
            ViewBag.courseName = context.courses?.Find(id)?.Description;
            ViewBag.Id = id.ToString();
            return View("ProfessorAssigning", context.professors.Include(p => p.Courses));
        }

        //This method may in best practice be put in another controller that is main method is to handle assigning courses.
        [HttpPost]
        public IActionResult AssignProfessor(int courseId, int profID)
        {
            if (courseId != 0 || profID != 0)
            {
                CoursesAndProfessors cp = new CoursesAndProfessors()
                {
                    CourseId = courseId,
                    ProfessorId = profID,
                };
                context.coursesAndProfessors.Add(cp);
                context.SaveChanges();
                return RedirectToAction("Index", "Courses");
            }
            return View("ProfessorAssigning");
        }

        [HttpGet]
        public IActionResult RegisterStudent(int id)
        {
            Course course = context.courses.Include(c => c.Professors).FirstOrDefault(c => c.Id == id);
            var result = course.Professors.Any(cp => cp.ProfessorId != 0);
            if (result == false)
            {
                ViewBag.PreventNonInstructorCourse =
"You Can't Register course " + course.Description + " as it does not has instructor";
                return View("Index", context.courses);
            }

            ViewBag.courseName = context.courses?.Find(id)?.Description;
            ViewBag.Id = id.ToString();
            return View("RegisterStudent", context.students?.Include(p => p.Courses));

        }

        [HttpPost]
        public IActionResult RegisterStudent(int sid, int cid)
        {
            if (sid != 0 || cid != 0)
            {
                StudentsAndCourses cp = new StudentsAndCourses()
                {
                    CourseId = cid,
                    StudentId = sid,
                };
                Student student = context.students.Find(sid);
                student.TotalPassedHours += context.courses.Find(cid).hours;
                context.StudentsAndCourses?.Add(cp);
                context.SaveChanges();
                return RedirectToAction("Index", "Courses");
            }
            return View("RegisterStudent");

        }


        [HttpGet]
        public IActionResult NotEvaluatedStudents(int id)
        {
            Course course = context.courses.Include(c => c.Students).ThenInclude(s => s.student).FirstOrDefault(c => c.Id == id);
            if (course == null)
                return NotFound();
            return View(course);
        }

        [HttpPost]
        public IActionResult EvaluteResultToStudent(int cid, int sid, int Grade)
        {
            if (cid == 0 || sid == 0)
                return NotFound();
            Course course = context.courses.Find(cid);
            Student student = context.students.Find(sid);
            StudentsAndCourses studentsAndCourses = new StudentsAndCourses() {
                CourseId = cid,
                StudentId = sid,
                Grade = Grade,
            };
            if (Grade >= 90)
                student.GPA += (4 * (double)course.hours) / student.TotalPassedHours;
            else if(Grade >= 85)
                student.GPA += (3.75 * course.hours) / student.TotalPassedHours;
            else if(Grade >= 80)
                student.GPA += (double)(3.4 * course.hours) / student.TotalPassedHours;
            else if(Grade >= 75)
                student.GPA += (double)(3.1 * course.hours) / student.TotalPassedHours;
            else if(Grade >= 70)
                student.GPA += (double)(2.8 * course.hours) / student.TotalPassedHours;
            else if(Grade >= 65)
                student.GPA += (double)(2.5 * course.hours) / student.TotalPassedHours;
            else if(Grade >= 60)
                student.GPA += (double)(2.25 * course.hours) / student.TotalPassedHours;
            else if(Grade >= 50)
                student.GPA += (double)(2 * course.hours) / student.TotalPassedHours;
            else
                student.GPA += (double)(0 * course.hours) / student.TotalPassedHours;

            if (student.TotalPassedHours >= 36 && student.TotalPassedHours <= 72)
                student.Level = 2;
            else if (student.TotalPassedHours > 72 && student.TotalPassedHours <= 108)
                student.Level = 3;
            else if (student.TotalPassedHours > 108 && student.TotalPassedHours <= 129)
                student.Level = 4;
            context.StudentsAndCourses.Update(studentsAndCourses);
            context.students.Update(student);
            context.SaveChanges();
            return RedirectToAction("Index", "Courses");
        }
    }
}