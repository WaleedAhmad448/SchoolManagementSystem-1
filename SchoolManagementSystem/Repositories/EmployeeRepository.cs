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

        // Obtém funcionários por departamento
        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(Department department)
        {
            return await _context.Employees
                .Where(e => e.Department == department)
                .ToListAsync();
        }

        // Obtém funcionários por status (Ativo, Inativo, Pendente)
        public async Task<IEnumerable<Employee>> GetEmployeesByStatusAsync(EmployeeStatus status)
        {
            return await _context.Employees
                .Where(e => e.Status == status)
                .ToListAsync();
        }

        // Obtém funcionários contratados nos últimos 30 dias
        public async Task<IEnumerable<Employee>> GetRecentlyHiredEmployeesAsync()
        {
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
            return await _context.Employees
                .Where(e => e.HireDate >= thirtyDaysAgo)
                .ToListAsync();
        }

        // Obtém funcionários administrativos (administração e recursos humanos)
        public async Task<IEnumerable<Employee>> GetAdministrativeEmployeesAsync()
        {
            return await _context.Employees
                .Where(e => e.Department == Department.Administration || e.Department == Department.HumanResources)
                .ToListAsync();
        }

        // Verifica se um funcionário pode gerir a criação de usuários
        public async Task<bool> CanEmployeeManageUserCreationAsync(int employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                return false;
            }

            // Verifica se o funcionário está ativo e em um departamento que pode criar usuários
            return employee.Status == EmployeeStatus.Active &&
                   (employee.Department == Department.Administration || employee.Department == Department.HumanResources);
        }

        // Conta o número de funcionários por departamento
        public async Task<int> CountEmployeesByDepartmentAsync(Department department)
        {
            return await _context.Employees
                .CountAsync(e => e.Department == department);
        }

        // Obtém funcionários com perfil completo (todos os campos preenchidos)
        public async Task<IEnumerable<Employee>> GetEmployeesWithCompleteProfileAsync()
        {
            return await _context.Employees
                .Where(e => !string.IsNullOrEmpty(e.FirstName) &&
                            !string.IsNullOrEmpty(e.LastName) &&
                            !string.IsNullOrEmpty(e.PhoneNumber) &&
                            e.HireDate != null &&
                            e.Department != null)
                .ToListAsync();
        }

        // Método que obtém todos os funcionários com dados completos (incluindo entidades relacionadas)
        public async Task<IEnumerable<Employee>> GetAllWithIncludesAsync()
        {
            return await _context.Employees
                .Include(e => e.User) // Inclui a entidade User associada ao funcionário
                                      // Se houver associações adicionais que você deseja incluir, adicione-as aqui
                .ToListAsync();
        }

        // Obtém um funcionário pelo UserId
        public async Task<Employee> GetEmployeeByUserIdAsync(string userId)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.UserId == userId);
        }

    }
}
