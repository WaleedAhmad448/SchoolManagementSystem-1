using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories;
using SchoolManagementSystem.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Controllers
{
    public class SchoolClassesController : Controller
    {
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<SchoolClassesController> _logger;

        public SchoolClassesController(
            ISchoolClassRepository schoolClassRepository,
            IConverterHelper converterHelper,
            ICourseRepository courseRepository,
            ILogger<SchoolClassesController> logger)
        {
            _schoolClassRepository = schoolClassRepository;
            _converterHelper = converterHelper;
            _courseRepository = courseRepository;
            _logger = logger;
        }

        // GET: SchoolClasses
        public async Task<IActionResult> Index()
        {
            var schoolClasses = await _schoolClassRepository.GetAllAsync();
            // Converte SchoolClasses para SchoolClassViewModels
            var viewModel = schoolClasses.Select(sc => _converterHelper.ToSchoolClassViewModel(sc)).ToList();
            return View(viewModel);
        }

        // GET: SchoolClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return new NotFoundViewResult("SchoolClassNotFound");

            var schoolClass = await _schoolClassRepository.GetClassWithSubjectsAsync(id.Value);
            if (schoolClass == null) return new NotFoundViewResult("SchoolClassNotFound");

            return View(_converterHelper.ToSchoolClassViewModel(schoolClass));
        }

        // GET: SchoolClasses/Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdownData();
            return View();
        }

        // POST: SchoolClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SchoolClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var schoolClass = await _converterHelper.ToSchoolClassAsync(model, true);
                    await _schoolClassRepository.CreateAsync(schoolClass);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error creating school class.");
                    ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                }
            }

            await LoadDropdownData(); // Recarregar os cursos no caso de erro
            return View(model);
        }

        // GET: SchoolClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return new NotFoundViewResult("SchoolClassNotFound");

            var schoolClass = await _schoolClassRepository.GetByIdAsync(id.Value);
            if (schoolClass == null) return new NotFoundViewResult("SchoolClassNotFound");

            var model = _converterHelper.ToSchoolClassViewModel(schoolClass);
            await LoadDropdownData();
            return View(model);
        }

        // POST: SchoolClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SchoolClassViewModel model)
        {
            if (id != model.Id) return new NotFoundViewResult("SchoolClassNotFound");

            if (ModelState.IsValid)
            {
                try
                {
                    var schoolClass = await _converterHelper.ToSchoolClassAsync(model, false);
                    await _schoolClassRepository.UpdateAsync(schoolClass);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SchoolClassExists(model.Id)) return new NotFoundViewResult("SchoolClassNotFound");
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating school class.");
                    ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                }
            }

            await LoadDropdownData(); // Recarregar os cursos no caso de erro
            return View(model);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return new NotFoundViewResult("SchoolClassNotFound");

            var schoolClass = await _schoolClassRepository.GetByIdAsync(id.Value);
            if (schoolClass == null) return new NotFoundViewResult("SchoolClassNotFound");

            return View(_converterHelper.ToSchoolClassViewModel(schoolClass));
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoolClass = await _schoolClassRepository.GetByIdAsync(id);
            if (schoolClass == null) return new NotFoundViewResult("SchoolClassNotFound");

            try
            {
                await _schoolClassRepository.DeleteAsync(schoolClass);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{schoolClass.ClassName} is being used!";
                    ViewBag.ErrorMessage = "This school class cannot be deleted because it has associated data.";
                }
                return View("Error");
            }
        }

        private async Task<bool> SchoolClassExists(int id)
        {
            return await _schoolClassRepository.ExistAsync(id);
        }

        private async Task LoadDropdownData()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            ViewBag.CourseId = new SelectList(courses, "Id", "CourseName");
        }

        public IActionResult SchoolClassNotFound()
        {
            return View();
        }
    }
}
