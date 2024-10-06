using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly SchoolDbContext _context;

        public TeacherRepository(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        // Obtém todos os professores com suas disciplinas associadas
        public async Task<IEnumerable<Teacher>> GetAllTeachersWithSubjectsAsync()
        {
            return await _context.Teachers
                .Include(t => t.TeacherSubjects)
                    .ThenInclude(ts => ts.Subject) // Inclui a entidade Subject através da entidade de junção
                .ToListAsync();
        }

        // Obtém professores que lecionam uma disciplina específica
        public async Task<IEnumerable<Teacher>> GetTeachersByDisciplineAsync(int subjectId)
        {
            return await _context.Teachers
                .Where(t => t.TeacherSubjects.Any(ts => ts.SubjectId == subjectId))
                .ToListAsync();
        }

        // Obtém um professor pelo nome completo
        public async Task<Teacher> GetTeacherByFullNameAsync(string fullName)
        {
            var names = fullName.Split(' '); // Supondo que o nome completo é fornecido como "FirstName LastName"
            return await _context.Teachers
                .FirstOrDefaultAsync(t => t.FirstName == names[0] && t.LastName == names[1]);
        }

        // Obtém um professor específico com suas disciplinas
        public async Task<Teacher> GetTeacherWithSubjectsAsync(int teacherId)
        {
            return await _context.Teachers
                .Include(t => t.TeacherSubjects)
                    .ThenInclude(ts => ts.Subject) // Inclui a entidade Subject através da entidade de junção
                .FirstOrDefaultAsync(t => t.Id == teacherId);
        }

        // Atualiza as disciplinas de um professor
        public async Task UpdateTeacherSubjectsAsync(int teacherId, IEnumerable<int> subjectIds)
        {
            var teacher = await _context.Teachers
                .Include(t => t.TeacherSubjects)
                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher != null)
            {
                // Limpa as disciplinas atuais
                teacher.TeacherSubjects.Clear();

                // Adiciona as novas disciplinas
                foreach (var subjectId in subjectIds)
                {
                    teacher.TeacherSubjects.Add(new TeacherSubject { TeacherId = teacherId, SubjectId = subjectId });
                }

                await _context.SaveChangesAsync();
            }
        }

        // Atualiza as turmas de um professor
        public async Task UpdateTeacherClassesAsync(int teacherId, IEnumerable<int> schoolClassIds)
        {
            var teacher = await _context.Teachers
                .Include(t => t.TeacherSchoolClasses)
                .FirstOrDefaultAsync(t => t.Id == teacherId);

            if (teacher != null)
            {
                // Limpa as turmas atuais
                teacher.TeacherSchoolClasses.Clear();

                // Adiciona as novas turmas
                foreach (var schoolClassId in schoolClassIds)
                {
                    teacher.TeacherSchoolClasses.Add(new TeacherSchoolClass { TeacherId = teacherId, SchoolClassId = schoolClassId });
                }

                await _context.SaveChangesAsync();
            }
        }


        // Obtém professores por status
        public async Task<IEnumerable<Teacher>> GetTeachersByStatusAsync(TeacherStatus status)
        {
            return await _context.Teachers
                .Where(t => t.Status == status)
                .ToListAsync();
        }

        // Conta professores por disciplina
        public async Task<int> CountTeachersByDisciplineAsync(int subjectId)
        {
            return await _context.Teachers
                .CountAsync(t => t.TeacherSubjects.Any(ts => ts.SubjectId == subjectId));
        }

        // Método que inclui disciplinas e turmas
        public async Task<IEnumerable<Teacher>> GetAllWithIncludesAsync()
        {
            return await _context.Teachers
                .Include(t => t.TeacherSubjects) // Inclui as disciplinas associadas
                    .ThenInclude(ts => ts.Subject) // Inclui as entidades de disciplina
                .Include(t => t.TeacherSchoolClasses) // Inclui a tabela de junção para turmas
                    .ThenInclude(tsc => tsc.SchoolClass) // Inclui as turmas associadas
                .ToListAsync();
        }


        // Método para obter todos os professores
        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetTeacherWithDetailsAsync(int teacherId)
        {
            return await _context.Teachers
                .Include(t => t.TeacherSchoolClasses)
                    .ThenInclude(tsc => tsc.SchoolClass)
                .Include(t => t.TeacherSubjects)
                    .ThenInclude(ts => ts.Subject)
                .FirstOrDefaultAsync(t => t.Id == teacherId);
        }

    }
}
