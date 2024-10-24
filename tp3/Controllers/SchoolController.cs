using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tp3.Models;
using tp3.Models.Repositories;

namespace tp3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class SchoolController : Controller
    {
        readonly ISchoolRepository schoolRepository;
        public SchoolController(ISchoolRepository schRepository)
        {
            schoolRepository = schRepository;
        }
        // GET: SchoolController
        [AllowAnonymous]
        public ActionResult Index()
        {
            var schools = schoolRepository.GetAll();
            return View(schools);
        }

        // GET: SchoolController/Details/5
        public ActionResult Details(int id)
        {
            var school = schoolRepository.GetById(id);
            return View(school);
        }

        // GET: SchoolController/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: SchoolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(School newschool)
        {
            try
            {
                schoolRepository.Add(newschool);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolController/Edit/5
        public ActionResult Edit(int id)
        {
            var school = schoolRepository.GetById(id);
            return View(school);
        }

        // POST: SchoolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(School newschool)
        {
            try
            {
                schoolRepository.Edit(newschool);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolController/Delete/5
        public ActionResult Delete(int id)
        {
            var school = schoolRepository.GetById(id);
            return View(school);
        }

        // POST: SchoolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(School newschool)
        {
            try
            {
                schoolRepository.Delete(newschool);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string term)
        {
            var schools = schoolRepository.Search(term);
            return View("Index", schools); // Return the Index view with the filtered list of schools
        }
    }
}
