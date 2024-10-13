using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        // Método para obter estudantes por ID da turma
        Task<IEnumerable<Student>> GetStudentsByClassIdAsync(int classId);

        // Método para obter estudantes pelo status
        Task<IEnumerable<Student>> GetStudentsByStatusAsync(string status);

        // Método para obter um estudante com seus cursos associados
        Task<Student> GetStudentWithCoursesAsync(int studentId);

        // Método para obter um estudante pelo nome completo
        Task<Student> GetByFullNameAsync(string fullName);

        // Método para obter todos os estudantes com as entidades relacionadas
        Task<IEnumerable<Student>> GetAllWithIncludesAsync();
    }
}
