using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories;
using SchoolManagementSystem.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(
            ICourseRepository courseRepository,
            IConverterHelper converterHelper,
            ISchoolClassRepository schoolClassRepository,
            ISubjectRepository subjectRepository,
            ILogger<CoursesController> logger)
        {
            _courseRepository = courseRepository;
            _converterHelper = converterHelper;
            _schoolClassRepository = schoolClassRepository;
            _subjectRepository = subjectRepository;
            _logger = logger;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return View(courses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return new NotFoundViewResult("CourseNotFound");

            var course = await _courseRepository.GetCourseWithClassesAsync(id.Value);
            if (course == null) return new NotFoundViewResult("CourseNotFound");

            return View(_converterHelper.ToCourseViewModel(course));
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            var model = new CourseViewModel
            {
                SubjectIds = new List<int>(), // Inicializando como lista vazia
                SchoolClassIds = new List<int>() // Inicializando como lista vazia
            };
            await LoadDropdownData();
            return View(model);
        }


        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownData();
                return View(model); // Retorna a view com os erros
            }

            try
            {
                var course = await _converterHelper.ToCourseAsync(model);
                await _courseRepository.CreateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred while creating course.");
                ModelState.AddModelError("", "Database error occurred. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating course.");
                ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
            }

            await LoadDropdownData();
            return View(model); // Retorna a view com os erros
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return new NotFoundViewResult("CourseNotFound");

            var course = await _courseRepository.GetByIdAsync(id.Value);
            if (course == null) return new NotFoundViewResult("CourseNotFound");

            var model = _converterHelper.ToCourseViewModel(course);
            await LoadDropdownData();
            return View(model);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel model)
        {
            if (id != model.Id) return new NotFoundViewResult("CourseNotFound");

            if (!ModelState.IsValid)
            {
                await LoadDropdownData();
                return View(model); // Retorna a view com os erros
            }

            try
            {
                var course = await _converterHelper.ToCourseAsync(model);
                await _courseRepository.UpdateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CourseExists(model.Id)) return new NotFoundViewResult("CourseNotFound");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating course.");
                ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
            }

            await LoadDropdownData();
            return View(model); // Retorna a view com os erros
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return new NotFoundViewResult("CourseNotFound");

            var course = await _courseRepository.GetByIdAsync(id.Value);
            if (course == null) return new NotFoundViewResult("CourseNotFound");

            return View(_converterHelper.ToCourseViewModel(course));
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return new NotFoundViewResult("CourseNotFound");

            try
            {
                await _courseRepository.DeleteAsync(course);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{course.CourseName} is being used!";
                    ViewBag.ErrorMessage = "This course cannot be deleted because it has associated data.";
                }
                return View("Error");
            }
        }

        private async Task<bool> CourseExists(int id)
        {
            return await _courseRepository.ExistAsync(id);
        }

        // Método para carregar dropdowns de Subjects e SchoolClasses
        private async Task LoadDropdownData()
        {
            // Carregar disciplinas (Subjects) - todas as disciplinas devem aparecer, mesmo se associadas a outros cursos
            var subjects = await _subjectRepository.GetAllAsync();

            // Carregar turmas (SchoolClasses) - apenas turmas que ainda não estão associadas a um curso, mas incluindo "No Class"
            var availableClasses = await _schoolClassRepository.GetAllAsync();
            var filteredClasses = availableClasses
                .Where(c => c.CourseId == null || c.ClassName == "No Class") // Filtrar turmas sem curso ou "No Class"
                .ToList();

            // Popular as dropdowns com os dados filtrados
            ViewBag.Subjects = subjects.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SubjectName
            }).ToList();

            ViewBag.SchoolClasses = filteredClasses.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.ClassName
            }).ToList();
        }


        public IActionResult CourseNotFound()
        {
            return View();
        }
    }
}
