using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId);
        Task<IEnumerable<Subject>> GetSubjectsByCourseIdAsync(int courseId);
        Task<bool> CheckForScheduleConflictsAsync(int classId, DateTime startTime, DateTime endTime);
        Task<Subject> GetByNameAsync(string subjectName);
        Task<IEnumerable<Subject>> GetAllAsync();

    }
}
