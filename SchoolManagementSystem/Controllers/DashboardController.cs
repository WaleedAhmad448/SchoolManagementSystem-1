using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Repositories;
using Syncfusion.EJ2.Charts;

namespace SchoolManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAlertRepository _alertRepository;

        public DashboardController(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            var activeAlerts = await _alertRepository.GetActiveAlertsAsync();
            ViewData["ActiveAlerts"] = activeAlerts;

            // Example data for graphs
            ViewData["StudentStatistics"] = new[] {
                new { Category = "Active Students", Value = 350 },
                new { Category = "Inactive Students", Value = 50 },
                new { Category = "Pending Students", Value = 20 }
            };

            return View();
        }

        // GET: Dashboard/ResolveAlert/5
        public async Task<IActionResult> ResolveAlert(int id)
        {
            var alert = await _alertRepository.GetByIdAsync(id);
            if (alert == null)
            {
                return NotFound();
            }

            alert.IsResolved = true;
            await _alertRepository.UpdateAsync(alert);

            return RedirectToAction(nameof(Index));
        }
    }
}