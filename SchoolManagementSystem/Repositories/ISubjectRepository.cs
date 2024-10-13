using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        // Method to retrieve subjects taught by a specific teacher
        Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId);

        // Method to retrieve subjects by course ID (currently commented out)
        // Task<IEnumerable<Subject>> GetSubjectsByCourseIdAsync(int courseId);

        // Method to check for schedule conflicts (currently commented out)
        // Task<bool> CheckForScheduleConflictsAsync(int classId, DateTime startTime, DateTime endTime);

        // Method to retrieve a subject by its name
        Task<Subject> GetByNameAsync(string subjectName);

        // Method to retrieve all subjects
        Task<IEnumerable<Subject>> GetAllAsync();
    }
}
