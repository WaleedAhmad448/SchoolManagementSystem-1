using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceController(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        // GET: Attendance
        public async Task<IActionResult> Index()
        {
            var attendanceRecords = await _attendanceRepository.GetAll().ToListAsync();
            var viewModel = attendanceRecords.Select(a => new AttendanceViewModel
            {
                Id = a.Id,
                StudentId = a.StudentId,
                StudentName = a.Student.User.FullName,
                SubjectId = a.SubjectId,
                SubjectName = a.Subject.SubjectName,
                Date = a.Date,
                Presence = a.Presence,
                Remarks = a.Remarks
            }).ToList();

            return View(viewModel);
        }

        // GET: Attendance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _attendanceRepository.GetByIdAsync(id.Value);
            if (attendance == null)
            {
                return NotFound();
            }

            var viewModel = new AttendanceViewModel
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                StudentName = attendance.Student.User.FullName,
                SubjectId = attendance.SubjectId,
                SubjectName = attendance.Subject.SubjectName,
                Date = attendance.Date,
                Presence = attendance.Presence,
                Remarks = attendance.Remarks
            };

            return View(viewModel);
        }

        // GET: Attendance/Mark
        public IActionResult Mark()
        {
            return View();
        }

        // POST: Attendance/Mark
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Mark(AttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var attendance = new Attendance
                {
                    StudentId = model.StudentId,
                    SubjectId = model.SubjectId,
                    Date = model.Date,
                    Presence = model.Presence,
                    Remarks = model.Remarks
                };

                await _attendanceRepository.MarkAttendanceAsync(attendance.StudentId, attendance.Date, attendance.Presence);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // Additional method to check student deletion
        public async Task<IActionResult> CheckExclusion(int studentId, int subjectId)
        {
            var attendanceRecords = await _attendanceRepository.GetAttendanceByStudentIdAsync(studentId);
            var totalClasses = attendanceRecords.Count();
            var absences = attendanceRecords.Count(a => !a.Presence);

            var viewModel = new AttendanceViewModel
            {
                StudentId = studentId,
                SubjectId = subjectId,
                IsExcluded = (double)absences / totalClasses * 100 > 25
            };

            return View(viewModel);
        }
    }
}
