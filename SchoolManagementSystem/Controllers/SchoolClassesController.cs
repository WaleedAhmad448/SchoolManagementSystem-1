using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories;
using SchoolManagementSystem.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SchoolManagementSystem.Controllers
{
    public class SchoolClassesController : Controller
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly ILogger<SchoolClassesController> _logger;

        public SchoolClassesController(
            ISchoolClassRepository schoolClassRepository,
            ICourseRepository courseRepository,
            IConverterHelper converterHelper,
            ILogger<SchoolClassesController> logger)
        {
            _schoolClassRepository = schoolClassRepository;
            _courseRepository = courseRepository;
            _converterHelper = converterHelper;
            _logger = logger;
        }

        // GET: SchoolClass
        public async Task<IActionResult> Index()
        {
            var schoolClasses = await _schoolClassRepository.GetAllAsync();
            return View(schoolClasses);
        }

        // GET: SchoolClass/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return new NotFoundViewResult("SchoolClassNotFound");

            var schoolClass = await _schoolClassRepository.GetClassWithStudentsAsync(id.Value);
            if (schoolClass == null) return new NotFoundViewResult("SchoolClassNotFound");

            var model = _converterHelper.ToSchoolClassViewModel(schoolClass);
            return View(model);
        }

        // GET: SchoolClass/Create
        public async Task<IActionResult> Create()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "CourseName");
            return View();
        }

        // POST: SchoolClass/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SchoolClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var schoolClass = await _converterHelper.ToSchoolClassAsync(model, true);
                await _schoolClassRepository.CreateAsync(schoolClass);
                return RedirectToAction(nameof(Index));
            }

            // Reload courses if there's a validation error
            var courses = await _courseRepository.GetAllCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "CourseName");
            return View(model);
        }

        // GET: SchoolClass/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return new NotFoundViewResult("SchoolClassNotFound");

            var schoolClass = await _schoolClassRepository.GetByIdAsync(id.Value);
            if (schoolClass == null) return new NotFoundViewResult("SchoolClassNotFound");

            var model = _converterHelper.ToSchoolClassViewModel(schoolClass);
            var courses = await _courseRepository.GetAllCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "CourseName", model.CourseId); // Preselect the course
            return View(model);
        }

        // POST: SchoolClass/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SchoolClassViewModel model)
        {
            if (id != model.Id) return new NotFoundViewResult("SchoolClassNotFound");

            if (ModelState.IsValid)
            {
                var schoolClass = await _converterHelper.ToSchoolClassAsync(model, false);
                await _schoolClassRepository.UpdateAsync(schoolClass);
                return RedirectToAction(nameof(Index));
            }

            var courses = await _courseRepository.GetAllCoursesAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "CourseName", model.CourseId);
            return View(model);
        }

        // GET: SchoolClass/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return new NotFoundViewResult("SchoolClassNotFound");

            var schoolClass = await _schoolClassRepository.GetClassWithStudentsAsync(id.Value);
            if (schoolClass == null) return new NotFoundViewResult("SchoolClassNotFound");

            var model = _converterHelper.ToSchoolClassViewModel(schoolClass);
            return View(model);
        }

        // POST: SchoolClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoolClass = await _schoolClassRepository.GetByIdAsync(id);
            if (schoolClass != null)
            {
                await _schoolClassRepository.DeleteAsync(schoolClass);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult SchoolClassNotFound()
        {
            return View();
        }
    }
}
