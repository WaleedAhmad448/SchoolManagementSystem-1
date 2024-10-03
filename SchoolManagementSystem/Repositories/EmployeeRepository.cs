using SchoolManagementSystem.Data;
using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly SchoolDbContext _context;

        public EmployeeRepository(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        // Implementation of the method to obtain employees by department
        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string department)
        {
            return await _context.Employees
                .Where(e => e.Department == department)
                .ToListAsync();
        }
    }
}
