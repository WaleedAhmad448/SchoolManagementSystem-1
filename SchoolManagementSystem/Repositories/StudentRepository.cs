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

        public async Task<Student> GetByFullNameAsync(string fullName)
        {
            return await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.User.FullName == fullName);
        }

        public async Task<IEnumerable<Student>> GetStudentsByClassIdAsync(int classId)
        {
            return await _context.Students
                .Where(s => s.SchoolClassId == classId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByStatusAsync(string status)
        {
            return await _context.Students
                .Where(s => s.Status == status)
                .ToListAsync();
        }

        public async Task<Student> GetStudentWithCoursesAsync(int studentId)
        {
            return await _context.Students
                .Include(s => s.SchoolClass)
                .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(s => s.Id == studentId);
        }

    }
}
