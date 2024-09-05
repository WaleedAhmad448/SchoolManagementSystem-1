using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Repositories;

namespace SchoolManagementSystem.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        // Injeção do repositório no construtor
        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            // Usando o repositório para obter todos os cursos
            var courses = await _courseRepository.GetAll().ToListAsync();
            return View(courses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Usando o repositório para obter os detalhes de um curso específico
            var course = await _courseRepository.GetByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseName,Description,Duration,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                // Usando o repositório para criar um novo curso
                await _courseRepository.CreateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Usando o repositório para obter um curso específico para edição
            var course = await _courseRepository.GetByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseName,Description,Duration,Credits")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Usando o repositório para atualizar o curso
                    await _courseRepository.UpdateAsync(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CourseExists(course.Id))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Usando o repositório para obter o curso a ser deletado
            var course = await _courseRepository.GetByIdAsync(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course != null)
            {
                // Usando o repositório para deletar o curso
                await _courseRepository.DeleteAsync(course);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CourseExists(int id)
        {
            // Usando o repositório para verificar se o curso existe
            return await _courseRepository.ExistAsync(id);
        }
    }
}
