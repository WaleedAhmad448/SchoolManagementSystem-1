using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Repositories;

namespace SchoolManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAlertRepository _alertRepository;

        public DashboardController(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> AdminDashboard()
        {
            return View();
        }

        public IActionResult EmployeeDashboard()
        {
            return View();
        }

        public IActionResult TeacherDashboard()
        {
            return View();
        }

        public IActionResult StudentDashboard()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
