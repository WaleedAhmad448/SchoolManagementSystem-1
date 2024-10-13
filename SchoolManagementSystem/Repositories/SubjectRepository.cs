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
            return await _context.TeacherSubjects
                .Where(ts => ts.TeacherId == teacherId)
                .Select(ts => ts.Subject)  
                .ToListAsync();
        }


        public async Task<Subject> GetByNameAsync(string subjectName)
        {
            return await _context.Subjects
                .FirstOrDefaultAsync(s => s.SubjectName == subjectName);
        }

        
        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

    }
}
