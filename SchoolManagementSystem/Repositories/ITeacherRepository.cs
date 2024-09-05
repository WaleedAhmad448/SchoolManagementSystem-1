using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Repositories
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetTeachersByDepartmentAsync(string department);
        Task<IEnumerable<Teacher>> GetTeachersByDisciplineAsync(int disciplineId);
        Task<Teacher> GetTeacherWithSubjectsAsync(int teacherId);
    }
}
