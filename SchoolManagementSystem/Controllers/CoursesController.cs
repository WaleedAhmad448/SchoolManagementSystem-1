using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Helpers;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IConverterHelper _converterHelper;

        public CoursesController(
            ICourseRepository courseRepository,
            ISchoolClassRepository schoolClassRepository,
            ISubjectRepository subjectRepository,
            IConverterHelper converterHelper)
        {
            _courseRepository = courseRepository;
            _schoolClassRepository = schoolClassRepository;
            _subjectRepository = subjectRepository;
            _converterHelper = converterHelper;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return View(courses);
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            var model = new CourseViewModel
            {
                AvailableSchoolClasses = (await _schoolClassRepository.GetAllAvailableAsync()).Select(sc => new SelectListItem
                {
                    Value = sc.Id.ToString(),
                    Text = sc.ClassName
                }),
                AvailableSubjects = (await _subjectRepository.GetAllAsync()).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SubjectName
                })
            };

            return View(model);
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repopula as listas disponíveis
                await PopulateAvailableClassesAndSubjects(model);
                return View(model);
            }

            try
            {
                var course = await _converterHelper.ToCourseAsync(model, true);
                await _courseRepository.CreateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while creating the course: {ex.Message}");
            }

            // Repopula as listas disponíveis em caso de erro
            await PopulateAvailableClassesAndSubjects(model);
            return View(model);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return NotFound();

            var model = _converterHelper.ToCourseViewModel(course);

            model.AvailableSchoolClasses = (await _schoolClassRepository.GetAllAvailableAsync()).Select(sc => new SelectListItem
            {
                Value = sc.Id.ToString(),
                Text = sc.ClassName,
                Selected = course.SchoolClasses.Any(cs => cs.Id == sc.Id) // Marca as classes associadas
            });

            model.AvailableSubjects = (await _subjectRepository.GetAllAsync()).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SubjectName,
                Selected = course.CourseSubjects.Any(cs => cs.SubjectId == s.Id) // Marca as disciplinas associadas
            });

            return View(model);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                // Repopula as listas disponíveis em caso de erro
                model.AvailableSchoolClasses = (await _schoolClassRepository.GetAllAvailableAsync()).Select(sc => new SelectListItem
                {
                    Value = sc.Id.ToString(),
                    Text = sc.ClassName
                });
                model.AvailableSubjects = (await _subjectRepository.GetAllAsync()).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SubjectName
                });

                return View(model);
            }

            try
            {
                var course = await _converterHelper.ToCourseAsync(model, false);
                await _courseRepository.UpdateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while updating the course: {ex.Message}");
            }

            // Repopula as listas disponíveis em caso de erro
            model.AvailableSchoolClasses = (await _schoolClassRepository.GetAllAvailableAsync()).Select(sc => new SelectListItem
            {
                Value = sc.Id.ToString(),
                Text = sc.ClassName
            });
            model.AvailableSubjects = (await _subjectRepository.GetAllAsync()).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SubjectName
            });

            return View(model);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepository.GetCourseWithClassesAndSubjectsAsync(id);
            if (course == null) return NotFound();

            var model = _converterHelper.ToCourseViewModel(course);
            return View(model);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return NotFound();

            try
            {
                await _courseRepository.DeleteAsync(course);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while deleting the course: {ex.Message}");
            }

            return View(course);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepository.GetCourseWithClassesAndSubjectsAsync(id);
            if (course == null) return NotFound();

            var model = _converterHelper.ToCourseViewModel(course);
            return View(model);
        }

        // Método para repopular as listas de turmas e disciplinas
        private async Task PopulateAvailableClassesAndSubjects(CourseViewModel model)
        {
            model.AvailableSchoolClasses = (await _schoolClassRepository.GetAllAvailableAsync())
                .Select(sc => new SelectListItem
                {
                    Value = sc.Id.ToString(),
                    Text = sc.ClassName
                });

            model.AvailableSubjects = (await _subjectRepository.GetAllAsync())
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SubjectName
                });
        }
    }
}
