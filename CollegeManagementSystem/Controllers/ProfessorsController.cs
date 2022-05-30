using CollegeManagementSystem.Data;
using CollegeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollegeManagementSystem.Controllers
{
    [Authorize]
    public class ProfessorsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProfessorsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }
        public IActionResult Index() => View(context.professors);

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Salary,Specialization,SSN,Age,imageUrl")]Professor professor, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                if (image == null)
                    professor.imageUrl = "\\images\\default.jpg";
                else
                {
                    string imgName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string imgUrl = "\\images\\professors\\" + imgName;
                    professor.imageUrl = imgUrl;
                    string imgPath = webHostEnvironment.WebRootPath + imgUrl;
                    FileStream stream = new FileStream(imgPath, FileMode.Create);
                    image.CopyTo(stream);
                    stream.Dispose();
                }
                context.professors?.Add(professor);
                context.SaveChanges();
                return RedirectToAction("Index", "Professors");
            }
            return View(professor);
        }

        [HttpGet]
        public IActionResult Details(int id) => View(context.professors?.Find(id));


        [HttpGet]
        public IActionResult Update(int id) => View(context.professors?.Find(id));

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([Bind("Id,Name,Salary,Specialization,SSN,Age,imageUrl")]Professor professor, int id, IFormFile? image)
        {
            if (id != professor.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                if (professor.imageUrl != "\\images\\default.jpg" && image != null)
                {
                    if (System.IO.File.Exists(webHostEnvironment.WebRootPath + professor.imageUrl))
                        System.IO.File.Delete(webHostEnvironment.WebRootPath + professor.imageUrl);
                }

                if (image != null)
                {
                    string imgName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string imgUrl = "\\images\\professors\\" + imgName;
                    professor.imageUrl = imgUrl;
                    string imgPath = webHostEnvironment.WebRootPath + imgUrl;
                    FileStream stream = new FileStream(imgPath, FileMode.Create);
                    image.CopyTo(stream);
                    stream.Dispose();
                }
                context.professors?.Update(professor);
                context.SaveChanges();
                return RedirectToAction("Index", "Professors");
            }
            else
                return View(professor);
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id) => View("Delete", context.professors?.Find(id));


        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == null)
                return View();
            else
            {
                Professor prof = context.professors?.Find(id);
                if (prof.imageUrl != "\\images\\default.jpg")
                {
                    if (System.IO.File.Exists(webHostEnvironment.WebRootPath + prof.imageUrl)) 
                    System.IO.File.Delete(webHostEnvironment.WebRootPath +  prof.imageUrl);
                }
                context.professors?.Remove(prof);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Professors");
        }

    }
}
