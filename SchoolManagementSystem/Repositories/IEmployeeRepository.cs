using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        // Método específico para obter funcionários por departamento
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string department);
    }
}
