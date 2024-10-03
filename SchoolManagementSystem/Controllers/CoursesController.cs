using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;

public class CoursesController : Controller
{
    private readonly ICourseRepository _courseRepository;

    public CoursesController(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _courseRepository.GetAll().ToListAsync();
        return View(courses);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _courseRepository.GetByIdAsync(id.Value);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CourseName,Description,Duration,Credits")] Course course)
    {
        if (ModelState.IsValid)
        {
            if ((await _courseRepository.GetCoursesByNameAsync(course.CourseName)).Any())
            {
                ModelState.AddModelError(string.Empty, "This course already exists.");
                return View(course);
            }

            await _courseRepository.CreateAsync(course);
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _courseRepository.GetByIdAsync(id.Value);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

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
                await _courseRepository.UpdateAsync(course);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CourseExists(course.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _courseRepository.GetByIdAsync(id.Value);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course != null)
        {
            await _courseRepository.DeleteAsync(course);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> CourseExists(int id)
    {
        return await _courseRepository.ExistAsync(id);
    }
}
