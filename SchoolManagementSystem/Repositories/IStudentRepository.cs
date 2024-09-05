using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentsByClassIdAsync(int classId);
        Task<IEnumerable<Student>> GetStudentsByStatusAsync(string status);
        Task<Student> GetStudentWithCoursesAsync(int studentId);
    }
}
