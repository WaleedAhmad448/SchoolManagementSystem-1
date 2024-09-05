using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly SchoolDbContext _context;

        public TeacherRepository(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teacher>> GetTeachersByDepartmentAsync(string department)
        {
            return await _context.Teachers
                .Where(t => t.Department == department)
                .ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> GetTeachersByDisciplineAsync(int disciplineId)
        {
            return await _context.Subjects
                .Where(s => s.Id == disciplineId)
                .Select(s => s.Teacher)
                .ToListAsync();
        }

        public async Task<Teacher> GetTeacherWithSubjectsAsync(int teacherId)
        {
            return await _context.Teachers
                .Include(t => t.Subjects)
                .FirstOrDefaultAsync(t => t.Id == teacherId);
        }
    }

}
