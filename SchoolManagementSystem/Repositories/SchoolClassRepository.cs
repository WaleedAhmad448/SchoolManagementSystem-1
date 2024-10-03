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

        public async Task<IEnumerable<SchoolClass>> GetClassesByCourseIdAsync(int courseId)
        {
            return await _context.SchoolClasses
                .Where(c => c.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<int> GetStudentCountByClassIdAsync(int classId)
        {
            return await _context.Students
                .CountAsync(s => s.SchoolClassId == classId);
        }

        public async Task<SchoolClass> GetClassWithSubjectsAsync(int classId)
        {
            return await _context.SchoolClasses
                .Include(c => c.Subjects)
                .FirstOrDefaultAsync(c => c.Id == classId);
        }

        public async Task<IEnumerable<SchoolClass>> GetAllAsync()
        {
            return await _context.SchoolClasses.ToListAsync();
        }
    }

}
