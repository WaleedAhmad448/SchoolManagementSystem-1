using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Repositories
{
    public class SchoolClassRepository : GenericRepository<SchoolClass>, ISchoolClassRepository
    {
        private readonly SchoolDbContext _context;

        public SchoolClassRepository(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        // Método para obter turmas associadas a um curso específico
        public async Task<IEnumerable<SchoolClass>> GetClassesByCourseIdAsync(int courseId)
        {
            return await _context.SchoolClasses
                .Where(c => c.CourseId == courseId)
                .ToListAsync();
        }

        // Método para contar o número de alunos em uma turma específica
        public async Task<int> GetStudentCountByClassIdAsync(int classId)
        {
            return await _context.Students
                .CountAsync(s => s.SchoolClassId == classId);
        }

        // Método para obter uma turma com suas disciplinas associadas
        public async Task<SchoolClass> GetClassWithSubjectsAsync(int classId)
        {
            return await _context.SchoolClasses
                .Include(c => c.Subjects)
                .FirstOrDefaultAsync(c => c.Id == classId);
        }

        // Método para obter todas as turmas
        public async Task<IEnumerable<SchoolClass>> GetAllAsync()
        {
            return await _context.SchoolClasses.ToListAsync();
        }

        // Método para obter turmas que não estão associadas a nenhum curso
        public async Task<IEnumerable<SchoolClass>> GetClassesWithoutCourseAsync()
        {
            return await _context.SchoolClasses
                .Where(c => c.CourseId == null)
                .ToListAsync();
        }

        // Método para filtrar turmas por período de datas
        public async Task<IEnumerable<SchoolClass>> GetClassesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.SchoolClasses
                .Where(c => c.StartDate >= startDate && c.EndDate <= endDate)
                .ToListAsync();
        }

        // Método para verificar se uma turma já está associada a um curso
        public async Task<bool> IsClassAssignedToCourseAsync(int classId)
        {
            return await _context.SchoolClasses
                .AnyAsync(c => c.Id == classId && c.CourseId != null);
        }

        // Método para obter uma turma com seus professores associados
        public async Task<SchoolClass> GetClassWithTeachersAsync(int classId)
        {
            return await _context.SchoolClasses
                .Include(c => c.TeacherSchoolClasses)
                .ThenInclude(tsc => tsc.Teacher)
                .FirstOrDefaultAsync(c => c.Id == classId);
        }

        // Método para obter uma turma com seus alunos associados
        public async Task<SchoolClass> GetClassWithStudentsAsync(int classId)
        {
            return await _context.SchoolClasses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classId);
        }
    }
}
