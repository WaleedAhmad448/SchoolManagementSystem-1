using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        private readonly SchoolDbContext _context;

        public SubjectRepository(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        // Método modificado para buscar Subjects via a entidade de junção TeacherSubject
        public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
        {
            return await _context.TeacherSubjects
                .Where(ts => ts.TeacherId == teacherId)
                .Select(ts => ts.Subject)  // Seleciona os Subjects relacionados
                .ToListAsync();
        }

        // Consulta baseada no CourseId continua válida
        public async Task<IEnumerable<Subject>> GetSubjectsByCourseIdAsync(int courseId)
        {
            return await _context.Subjects
                .Where(s => s.CourseId == courseId)
                .ToListAsync();
        }

        // Verifica conflitos de horário
        public async Task<bool> CheckForScheduleConflictsAsync(int classId, DateTime startTime, DateTime endTime)
        {
            return await _context.Subjects
                .AnyAsync(s => s.SchoolClassId == classId && s.StartTime < endTime && s.EndTime > startTime);
        }

        // Busca por nome da disciplina
        public async Task<Subject> GetByNameAsync(string subjectName)
        {
            return await _context.Subjects
                .FirstOrDefaultAsync(s => s.SubjectName == subjectName);
        }

        // Obtém todos os subjects
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

    }
}
