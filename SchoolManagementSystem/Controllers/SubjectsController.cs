using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Repositories;

namespace SchoolManagementSystem.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;

        // Injeção dos repositórios
        public SubjectsController(ISubjectRepository subjectRepository, ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _subjectRepository = subjectRepository;
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var subjects = await _subjectRepository.GetAll().ToListAsync();
            return View(subjects);
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _subjectRepository.GetByIdAsync(id.Value);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subjects/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CourseId"] = new SelectList(await _courseRepository.GetAll().ToListAsync(), "Id", "CourseName");
            ViewData["TeacherId"] = new SelectList(await _teacherRepository.GetAll().ToListAsync(), "Id", "FullName");
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubjectName,Description,CourseId,TeacherId,StartTime,EndTime,SchoolClassId")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectRepository.CreateAsync(subject);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(await _courseRepository.GetAll().ToListAsync(), "Id", "CourseName", subject.CourseId);
            ViewData["TeacherId"] = new SelectList(await _teacherRepository.GetAll().ToListAsync(), "Id", "FullName", subject.TeacherId);
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _subjectRepository.GetByIdAsync(id.Value);
            if (subject == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(await _courseRepository.GetAll().ToListAsync(), "Id", "CourseName", subject.CourseId);
            ViewData["TeacherId"] = new SelectList(await _teacherRepository.GetAll().ToListAsync(), "Id", "FullName", subject.TeacherId);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectName,Description,CourseId,TeacherId,StartTime,EndTime,SchoolClassId")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subjectRepository.UpdateAsync(subject);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SubjectExists(subject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(await _courseRepository.GetAll().ToListAsync(), "Id", "CourseName", subject.CourseId);
            ViewData["TeacherId"] = new SelectList(await _teacherRepository.GetAll().ToListAsync(), "Id", "FullName", subject.TeacherId);
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _subjectRepository.GetByIdAsync(id.Value);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject != null)
            {
                await _subjectRepository.DeleteAsync(subject);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SubjectExists(int id)
        {
            return await _subjectRepository.ExistAsync(id);
        }
    }
}