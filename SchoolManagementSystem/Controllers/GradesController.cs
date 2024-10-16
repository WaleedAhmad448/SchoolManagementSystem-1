using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Helpers;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Controllers
{
    public class GradesController : Controller
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<GradesController> _logger;

        public GradesController(
            IGradeRepository gradeRepository,
            IStudentRepository studentRepository,
            ISchoolClassRepository schoolClassRepository,
            IConverterHelper converterHelper,
            ISubjectRepository subjectRepository,
            ILogger<GradesController> logger)
        {
            _gradeRepository = gradeRepository;
            _studentRepository = studentRepository;
            _schoolClassRepository = schoolClassRepository;
            _converterHelper = converterHelper;
            _subjectRepository = subjectRepository;
            _logger = logger;
        }

        // GET: Grades
        public async Task<IActionResult> Index(int? classId)
        {
            try
            {
                var schoolClasses = await _schoolClassRepository.GetAllAsync();
                ViewBag.Classes = schoolClasses.Select(sc => new SelectListItem
                {
                    Value = sc.Id.ToString(),
                    Text = sc.ClassName
                }).ToList();

                if (classId.HasValue)
                {
                    var students = await _studentRepository.GetStudentsBySchoolClassIdAsync(classId.Value);
                    var studentAverages = students.Select(student => new StudentGradeAverageViewModel
                    {
                        Student = student // Inclui a entidade Student, que contém as grades
                    }).ToList();

                    return View(studentAverages);
                }

                return View(new List<StudentGradeAverageViewModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading grades list."); // Log the error
                ModelState.AddModelError(string.Empty, "An error occurred while loading the grades list. Please try again later.");
                return View(new List<StudentGradeAverageViewModel>());
            }
        }

        // GET: Grades/Details/5
        public async Task<IActionResult> Details(int studentId)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(studentId); // Get the student
                var subjects = await _gradeRepository.GetSubjectsByStudentIdAsync(studentId);
                var grades = await _gradeRepository.GetGradesByStudentIdAsync(studentId);

                var model = subjects.Select(subject => new StudentSubjectGradeViewModel
                {
                    Subject = subject,
                    Grade = grades.FirstOrDefault(g => g.SubjectId == subject.Id),
                    StudentId = studentId,
                    StudentName = $"{student.FirstName} {student.LastName}" // Populate the student's name
                }).ToList();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading student details."); // Log the error
                ModelState.AddModelError(string.Empty, "An error occurred while loading student details. Please try again later.");
                return View(new List<StudentSubjectGradeViewModel>());
            }
        }

        // GET: Grades/AddGrade/5
        public async Task<IActionResult> AddGrade(int studentId, int subjectId)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(studentId);
                var subject = await _subjectRepository.GetByIdAsync(subjectId);

                var model = new GradeViewModel
                {
                    StudentId = studentId,
                    SubjectId = subjectId,
                    StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "Unknown",
                    SubjectName = subject != null ? subject.Name : "Unknown"
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading data to add a grade."); // Log the error
                ModelState.AddModelError(string.Empty, "An error occurred while loading data to add a grade. Please try again later.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Grades/AddGrade
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGrade(GradeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var grade = await _converterHelper.ToGradeAsync(model, true); // 'true' indicates it's a new grade
                    await _gradeRepository.CreateAsync(grade);
                    return RedirectToAction(nameof(Details), new { studentId = model.StudentId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding grade."); // Log the error
                    ModelState.AddModelError(string.Empty, "An error occurred while adding the grade. Please try again later.");
                }
            }

            return View(model); // Return the same view if the model is not valid
        }

        // GET: Grades/EditGrade/5
        public async Task<IActionResult> EditGrade(int id)
        {
            try
            {
                var grade = await _gradeRepository.GetGradeWithDetailsByIdAsync(id);
                if (grade == null)
                {
                    return NotFound();
                }

                var model = _converterHelper.ToGradeViewModel(grade);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading grade for editing."); // Log the error
                ModelState.AddModelError(string.Empty, "An error occurred while loading the grade for editing. Please try again later.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Grades/EditGrade
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGrade(GradeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var grade = await _converterHelper.ToGradeAsync(model, false);
                    await _gradeRepository.UpdateAsync(grade);
                    return RedirectToAction(nameof(Details), new { studentId = model.StudentId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating grade."); // Log the error
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the grade. Please try again later.");
                }
            }
            return View(model);
        }

        // GET: Grades/DeleteGrade/5
        public async Task<IActionResult> DeleteGrade(int id)
        {
            try
            {
                var grade = await _gradeRepository.GetGradeWithDetailsByIdAsync(id);
                if (grade == null)
                {
                    return NotFound();
                }
                return View(_converterHelper.ToGradeViewModel(grade));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading grade for deletion."); // Log the error
                ModelState.AddModelError(string.Empty, "An error occurred while loading the grade for deletion. Please try again later.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Grades/DeleteGrade
        [HttpPost, ActionName("DeleteGrade")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGradeConfirmed(int id)
        {
            try
            {
                var grade = await _gradeRepository.GetGradeWithDetailsByIdAsync(id);
                if (grade != null)
                {
                    await _gradeRepository.DeleteAsync(grade);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting grade."); // Log the error
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the grade. Please try again later.");
                return RedirectToAction(nameof(Index));
            }
        }

        // Method for Student
        // GET: Grades/MyGrades
        public async Task<IActionResult> MyGrades()
        {
            try
            {
                // Get the ID of the logged-in user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Get the StudentId from the UserId
                var studentId = await _studentRepository.GetStudentIdByUserIdAsync(userId);

                // Check if the studentId is null
                if (studentId == null)
                {
                    ModelState.AddModelError(string.Empty, "Student not found.");
                    return View(new List<StudentSubjectGradeViewModel>());
                }

                // Get the subjects and grades of the student
                var subjects = await _gradeRepository.GetSubjectsByStudentIdAsync(studentId.Value); // Use .Value to access the int
                var grades = await _gradeRepository.GetGradesByStudentIdAsync(studentId.Value);

                // Get the student to use their name
                var student = await _studentRepository.GetByIdAsync(studentId.Value);
                var model = subjects.Select(subject => new StudentSubjectGradeViewModel
                {
                    Subject = subject,
                    Grade = grades.FirstOrDefault(g => g.SubjectId == subject.Id),
                    StudentId = studentId.Value, // Use .Value to ensure it's an int
                    StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "Unknown" // Filling in the student's name
                }).ToList();

                return View(model); // Return the view that displays the grades
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading student's grades."); // Log the error
                ModelState.AddModelError(string.Empty, "An error occurred while loading your grades. Please try again later.");
                return View(new List<StudentSubjectGradeViewModel>());
            }
        }




    }
}
