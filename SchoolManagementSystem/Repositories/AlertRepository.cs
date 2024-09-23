using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public class AlertRepository : GenericRepository<Alert>, IAlertRepository
    {
        private readonly SchoolDbContext _context;

        public AlertRepository(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alert>> GetActiveAlertsAsync()
        {
            return await _context.Alerts
                .Where(a => !a.IsResolved)
                .ToListAsync();
        }
    }
}
