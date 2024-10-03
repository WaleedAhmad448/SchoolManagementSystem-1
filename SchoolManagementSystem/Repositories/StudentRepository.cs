using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly SchoolDbContext _context;

        public StudentRepository(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        // Método para obter todos os estudantes com as entidades relacionadas
        public async Task<IEnumerable<Student>> GetAllWithIncludesAsync()
        {
            return await _context.Students
                .Include(s => s.User)             // Incluir a entidade User
                .Include(s => s.SchoolClass)      // Incluir a entidade SchoolClass
                .AsNoTracking()                   // Não rastrear as entidades
                .ToListAsync();
        }

        // Método para obter um estudante pelo nome completo
        public async Task<Student> GetByFullNameAsync(string fullName)
        {
            return await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => $"{s.User.FirstName} {s.User.LastName}" == fullName);
        }

        // Método para obter estudantes por ID da turma
        public async Task<IEnumerable<Student>> GetStudentsByClassIdAsync(int classId)
        {
            return await _context.Students
                .Where(s => s.SchoolClassId == classId)
                .Include(s => s.SchoolClass) // Incluir a turma
                .Include(s => s.User)        // Incluir o usuário
                .ToListAsync();
        }

        // Método para obter estudantes pelo status
        public async Task<IEnumerable<Student>> GetStudentsByStatusAsync(string status)
        {
            // Converte a string status para o enum StudentStatus
            if (Enum.TryParse<StudentStatus>(status, out var studentStatus))
            {
                return await _context.Students
                    .Where(s => s.Status == studentStatus)  // Comparação usando enum
                    .Include(s => s.SchoolClass)            // Incluir a turma
                    .Include(s => s.User)                   // Incluir o usuário
                    .ToListAsync();
            }
            else
            {
                // Caso o status fornecido não seja válido, retorna uma lista vazia ou lança uma exceção
                return new List<Student>();
            }
        }

        // Método para obter um estudante com cursos
        public async Task<Student> GetStudentWithCoursesAsync(int studentId)
        {
            return await _context.Students
                .Include(s => s.SchoolClass)
                .ThenInclude(c => c.Course)
                .Include(s => s.User) 
                .FirstOrDefaultAsync(s => s.Id == studentId);
        }
    }
}
