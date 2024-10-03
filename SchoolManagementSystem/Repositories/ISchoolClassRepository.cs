using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Repositories
{
    public interface ISchoolClassRepository : IGenericRepository<SchoolClass>
    {
        Task<IEnumerable<SchoolClass>> GetClassesByCourseIdAsync(int courseId);
        Task<int> GetStudentCountByClassIdAsync(int classId);
        Task<SchoolClass> GetClassWithSubjectsAsync(int classId);
        Task<IEnumerable<SchoolClass>> GetAllAsync();

    }

}
