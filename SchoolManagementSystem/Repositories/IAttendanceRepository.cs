using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public interface IAttendanceRepository : IGenericRepository<Attendance>
    {
        Task<IEnumerable<Attendance>> GetAttendanceByStudentIdAsync(int studentId);
        Task MarkAttendanceAsync(int studentId, DateTime date, bool presence);
    }
}
