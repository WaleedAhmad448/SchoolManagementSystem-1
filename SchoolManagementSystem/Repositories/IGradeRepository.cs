using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId);
        Task AddGradeAsync(Grade grade);
    }
}
