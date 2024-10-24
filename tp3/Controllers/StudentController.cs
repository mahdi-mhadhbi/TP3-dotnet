using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tp3.Models.Repositories;
using tp3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace tp3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]

    public class StudentController : Controller
    {
        readonly ISchoolRepository schoolRepository;

        readonly IStudentRepository studentRepository;
        public StudentController(IStudentRepository stdRepository, ISchoolRepository schRepository)
        {
            studentRepository = stdRepository;
            schoolRepository = schRepository;
        }
        [AllowAnonymous]
        // GET: StudentController
        public ActionResult Index()
        {
            var schools = schoolRepository.GetAll();
            ViewBag.Schools = new SelectList(schools, "SchoolID", "SchoolName");
            var Students = studentRepository.GetAll();
            return View(Students);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var Student = studentRepository.GetById(id);
            return View(Student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            var schools = schoolRepository.GetAll();
            if (schools != null && schools.Any())
            {
                ViewBag.SchoolID = new SelectList(schools, "SchoolID", "SchoolName");
            }
            else
            {
                // Handle the case where no schools are found
                ViewBag.SchoolID = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            }

            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student newStudent)
        {
            try
            {
                var schools = schoolRepository.GetAll();
                if (schools != null && schools.Any())
                {
                    ViewBag.SchoolID = new SelectList(schools, "SchoolID", "SchoolName");
                }
                else
                {
                    // Handle the case where no schools are found
                    ViewBag.SchoolID = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                }
                studentRepository.Add(newStudent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var schools = schoolRepository.GetAll();
            if (schools != null && schools.Any())
            {
                ViewBag.SchoolID = new SelectList(schools, "SchoolID", "SchoolName");
            }
            else
            {
                // Handle the case where no schools are found
                ViewBag.SchoolID = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
            }
            var Student = studentRepository.GetById(id);
            return View(Student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student newStudent)
        {
            try
            {
                var schools = schoolRepository.GetAll();
                if (schools != null && schools.Any())
                {
                    ViewBag.SchoolID = new SelectList(schools, "SchoolID", "SchoolName");
                }
                else
                {
                    // Handle the case where no schools are found
                    ViewBag.SchoolID = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");
                }
                studentRepository.Edit(newStudent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var Student = studentRepository.GetById(id);
            return View(Student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Student newStudent)
        {
            try
            {
                studentRepository.Delete(newStudent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string term)
        {
            var students = studentRepository.Search(term);
            return View("Index", students); // Return the Index view with the filtered list of students
        }
        public IActionResult SearchBySchool(int? selectedSchoolId)
        {
            List<Student> students = new List<Student>();

            if (selectedSchoolId.HasValue)
            {
                students = studentRepository.GetStudentsBySchoolID(selectedSchoolId.Value).ToList();
            }

            var schools = schoolRepository.GetAll();
            ViewBag.Schools = new SelectList(schools, "SchoolID", "SchoolName");

            return View("Index", students);  // Returning filtered students to the Index view
        }

    }
}
