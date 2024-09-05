using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Repositories;

namespace SchoolManagementSystem.Controllers
{
    public class SchoolClassesController : Controller
    {
        private readonly ISchoolClassRepository _classRepository;
        private readonly ICourseRepository _courseRepository;

        // Injeção dos repositórios no construtor
        public SchoolClassesController(ISchoolClassRepository classRepository, ICourseRepository courseRepository)
        {
            _classRepository = classRepository;
            _courseRepository = courseRepository;
        }

        // GET: SchoolClasses
        public async Task<IActionResult> Index()
        {
            // Usando o repositório para obter as turmas com seus respectivos cursos
            var classes = await _classRepository.GetAll()
                .Include(c => c.Course)
                .ToListAsync();
            return View(classes);
        }

        // GET: SchoolClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _classRepository.GetAll()
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        // GET: SchoolClasses/Create
        public IActionResult Create()
        {
            // Usando o repositório de cursos para popular o dropdown
            ViewData["CourseId"] = new SelectList(_courseRepository.GetAll(), "Id", "CourseName");
            return View();
        }

        // POST: SchoolClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassName,CourseId,StartDate,EndDate")] SchoolClass schoolClass)
        {
            if (ModelState.IsValid)
            {
                await _classRepository.CreateAsync(schoolClass);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_courseRepository.GetAll(), "Id", "CourseName", schoolClass.CourseId);
            return View(schoolClass);
        }

        // GET: SchoolClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _classRepository.GetByIdAsync(id.Value);
            if (schoolClass == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_courseRepository.GetAll(), "Id", "CourseName", schoolClass.CourseId);
            return View(schoolClass);
        }

        // POST: SchoolClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassName,CourseId,StartDate,EndDate")] SchoolClass schoolClass)
        {
            if (id != schoolClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _classRepository.UpdateAsync(schoolClass);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ClassExists(schoolClass.Id))
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
            ViewData["CourseId"] = new SelectList(_courseRepository.GetAll(), "Id", "CourseName", schoolClass.CourseId);
            return View(schoolClass);
        }

        // GET: SchoolClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schoolClass = await _classRepository.GetAll()
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            return View(schoolClass);
        }

        // POST: SchoolClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schoolClass = await _classRepository.GetByIdAsync(id);
            if (schoolClass != null)
            {
                await _classRepository.DeleteAsync(schoolClass);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ClassExists(int id)
        {
            return await _classRepository.ExistAsync(id);
        }
    }
}
