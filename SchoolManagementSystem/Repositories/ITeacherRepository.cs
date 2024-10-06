using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Repositories
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        // Novo método para obter todos os professores com disciplinas
        Task<IEnumerable<Teacher>> GetAllTeachersWithSubjectsAsync();

        // Obter professores por disciplina, com retorno mais específico
        Task<IEnumerable<Teacher>> GetTeachersByDisciplineAsync(int subjectId);

        // Novo método para obter professor pelo nome completo
        Task<Teacher> GetTeacherByFullNameAsync(string fullName);

        // Obter professor específico com suas disciplinas associadas
        Task<Teacher> GetTeacherWithSubjectsAsync(int teacherId);

        // Atualizar as disciplinas de um professor
        Task UpdateTeacherSubjectsAsync(int teacherId, IEnumerable<int> subjectIds);

        // Novo método para obter professores por status
        Task<IEnumerable<Teacher>> GetTeachersByStatusAsync(TeacherStatus status);

        // Novo método para contar professores por disciplina
        Task<int> CountTeachersByDisciplineAsync(int subjectId);

        Task<IEnumerable<Teacher>> GetAllWithIncludesAsync(); // Método que inclui disciplinas e turmas
        Task<IEnumerable<Teacher>> GetAllAsync(); // Para obter todos os professores

        Task<Teacher> GetTeacherWithDetailsAsync(int teacherId);

        Task UpdateTeacherClassesAsync(int teacherId, IEnumerable<int> subjectIds);
        

    }
}
