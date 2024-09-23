using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        private readonly SchoolDbContext _context;

        public AttendanceRepository(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetAttendanceByStudentIdAsync(int studentId)
        {
            return await _context.Attendance
                .Where(a => a.StudentId == studentId)
                .ToListAsync();
        }

        public async Task MarkAttendanceAsync(int studentId, DateTime date, bool presence)
        {
            var attendance = new Attendance
            {
                StudentId = studentId,
                Date = date,
                Presence = presence
            };
            await _context.Attendance.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }
    }
}
