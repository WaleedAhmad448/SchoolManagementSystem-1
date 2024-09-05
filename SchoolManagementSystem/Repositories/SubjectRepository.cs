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

        public async Task<IEnumerable<Subject>> GetSubjectsByTeacherIdAsync(int teacherId)
        {
            return await _context.Subjects
                .Where(s => s.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByCourseIdAsync(int courseId)
        {
            return await _context.Subjects
                .Where(s => s.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<bool> CheckForScheduleConflictsAsync(int classId, DateTime startTime, DateTime endTime)
        {
            return await _context.Subjects
                .AnyAsync(s => s.SchoolClassId == classId && s.StartTime < endTime && s.EndTime > startTime);
        }
    }
}
