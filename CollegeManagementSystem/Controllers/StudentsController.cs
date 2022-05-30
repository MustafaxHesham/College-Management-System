using CollegeManagementSystem.Data;
using CollegeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public StudentsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }
        public IActionResult Index() => View(context.students);

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,SSN,Age,imageUrl")] Student student, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                if (image == null)
                    student.imageUrl = "\\images\\default.jpg";
                else
                {
                    string imgName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string imgUrl = "\\images\\students\\" + imgName;
                    student.imageUrl = imgUrl;
                    string imgPath = webHostEnvironment.WebRootPath + imgUrl;
                    FileStream stream = new FileStream(imgPath, FileMode.Create);
                    image.CopyTo(stream);
                    stream.Dispose();
                }
                context.students?.Add(student);
                context.SaveChanges();
                return RedirectToAction("Index", "Students");
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Details(int id) => View(context.students?.Find(id));


        [HttpGet]
        public IActionResult Update(int id) => View(context.students?.Find(id));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([Bind("Id,Name,SSN,Age,imageUrl")] Student student, int id, IFormFile? image)
        {
            if (id != student.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                if (student.imageUrl != "\\images\\default.jpg" && image != null)
                {
                    if (System.IO.File.Exists(webHostEnvironment.WebRootPath + student.imageUrl))
                        System.IO.File.Delete(webHostEnvironment.WebRootPath + student.imageUrl);
                }

                if (image != null)
                {
                    string imgName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string imgUrl = "\\images\\students\\" + imgName;
                    student.imageUrl = imgUrl;
                    string imgPath = webHostEnvironment.WebRootPath + imgUrl;
                    FileStream stream = new FileStream(imgPath, FileMode.Create);
                    image.CopyTo(stream);
                    stream.Dispose();
                }
                context.students?.Update(student);
                context.SaveChanges();
                return RedirectToAction("Index", "Students");
            }
            else
                return View(student);
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id) => View("Delete", context.students?.Find(id));


        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == null)
                return View();
            else
            {
                Student? student = context.students?.Find(id);
                if (student.imageUrl != "\\images\\default.jpg")
                {
                    if (System.IO.File.Exists(webHostEnvironment.WebRootPath + student.imageUrl))
                        System.IO.File.Delete(webHostEnvironment.WebRootPath + student.imageUrl);
                }
                context.students?.Remove(student);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Students");
        }
    }
}
