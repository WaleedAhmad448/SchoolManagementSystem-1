//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SchoolManagementSystem.Data.Entities;
//using SchoolManagementSystem.Repositories;
//using SchoolManagementSystem.Models;

//namespace SchoolManagementSystem.Controllers
//{
//    public class GradesController : Controller
//    {
//        private readonly IGradeRepository _gradeRepository;
//        private readonly IStudentRepository _studentRepository;
//        private readonly ISubjectRepository _subjectRepository;

//        public GradesController(IGradeRepository gradeRepository, IStudentRepository studentRepository, ISubjectRepository subjectRepository)
//        {
//            _gradeRepository = gradeRepository;
//            _studentRepository = studentRepository;
//            _subjectRepository = subjectRepository;
//        }

//        // GET: Grades
//        public async Task<IActionResult> Index()
//        {
//            var grades = await _gradeRepository.GetAll().ToListAsync();
//            var gradeViewModels = grades.Select(g => new GradeViewModel
//            {
//                Id = g.Id,
//                StudentName = g.Student.User.FullName,
//                SubjectName = g.Subject.Name,
//                Value = g.Value,
//                Status = g.Status,
//                DateRecorded = g.DateRecorded
//            });

//            return View(gradeViewModels);
//        }

//        // GET: Grades/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var grade = await _gradeRepository.GetByIdAsync(id.Value);
//            if (grade == null)
//            {
//                return NotFound();
//            }

//            var gradeViewModel = new GradeViewModel
//            {
//                Id = grade.Id,
//                StudentName = grade.Student.User.FullName,
//                SubjectName = grade.Subject.Name,
//                Value = grade.Value,
//                Status = grade.Status,
//                DateRecorded = grade.DateRecorded
//            };

//            return View(gradeViewModel);
//        }

//        // GET: Grades/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Grades/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(GradeViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var grade = new Grade
//                {
//                    StudentId = (await _studentRepository.GetByFullNameAsync(model.StudentName)).Id,
//                    SubjectId = (await _subjectRepository.GetByNameAsync(model.SubjectName)).Id,
//                    Value = model.Value,
//                    Status = model.Status,
//                    DateRecorded = model.DateRecorded
//                };

//                await _gradeRepository.AddGradeAsync(grade);
//                return RedirectToAction(nameof(Index));
//            }
//            return View(model);
//        }

//        // GET: Grades/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var grade = await _gradeRepository.GetByIdAsync(id.Value);
//            if (grade == null)
//            {
//                return NotFound();
//            }

//            var gradeViewModel = new GradeViewModel
//            {
//                Id = grade.Id,
//                StudentName = grade.Student.User.FullName,
//                SubjectName = grade.Subject.Name,
//                Value = grade.Value,
//                Status = grade.Status,
//                DateRecorded = grade.DateRecorded
//            };

//            return View(gradeViewModel);
//        }

//        // POST: Grades/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, GradeViewModel model)
//        {
//            if (id != model.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var grade = await _gradeRepository.GetByIdAsync(id);
//                    grade.Value = model.Value;
//                    grade.Status = model.Status;
//                    grade.DateRecorded = model.DateRecorded;

//                    await _gradeRepository.UpdateAsync(grade);
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!await GradeExists(model.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(model);
//        }

//        private async Task<bool> GradeExists(int id)
//        {
//            return await _gradeRepository.ExistAsync(id);
//        }
//    }
//}
