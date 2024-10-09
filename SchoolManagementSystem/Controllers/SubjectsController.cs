using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolManagementSystem.Helpers;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories;
using System;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly ILogger<SubjectsController> _logger;

        public SubjectsController(
            ISubjectRepository subjectRepository,
            IConverterHelper converterHelper,
            ILogger<SubjectsController> logger)
        {
            _subjectRepository = subjectRepository;
            _converterHelper = converterHelper;
            _logger = logger;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            return View(subjects);
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return new NotFoundViewResult("SubjectNotFound");

            var subject = await _subjectRepository.GetByIdAsync(id.Value);
            if (subject == null) return new NotFoundViewResult("SubjectNotFound"); 

            return View(_converterHelper.ToSubjectViewModel(subject));
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var subject = await _converterHelper.ToSubjectAsync(model);
                    await _subjectRepository.CreateAsync(subject);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating subject.");
                    ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                }
            }

            return View(model); // Retorna a view com erros de validação
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return new NotFoundViewResult("SubjectNotFound");

            var subject = await _subjectRepository.GetByIdAsync(id.Value);
            if (subject == null) return new NotFoundViewResult("SubjectNotFound");

            var model = _converterHelper.ToSubjectViewModel(subject);
            return View(model);
        }

        // POST: Subjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubjectViewModel model)
        {
            if (id != model.Id) return new NotFoundViewResult("SubjectNotFound");

            if (ModelState.IsValid)
            {
                try
                {
                    var subject = await _converterHelper.ToSubjectAsync(model);
                    await _subjectRepository.UpdateAsync(subject);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating subject.");
                    ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                }
            }

            return View(model); // Retorna a view com erros de validação
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return new NotFoundViewResult("SubjectNotFound");

            var subject = await _subjectRepository.GetByIdAsync(id.Value);
            if (subject == null) return new NotFoundViewResult("SubjectNotFound");

            return View(_converterHelper.ToSubjectViewModel(subject));
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null) return new NotFoundViewResult("SubjectNotFound");

            try
            {
                await _subjectRepository.DeleteAsync(subject);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting subject.");
                ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                return View("Error");
            }
        }

        public IActionResult SubjectNotFound()
        {
            return View();
        }
    }
}
