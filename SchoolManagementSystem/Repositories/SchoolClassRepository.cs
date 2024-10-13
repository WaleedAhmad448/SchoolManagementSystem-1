using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public class SchoolClassRepository : GenericRepository<SchoolClass>, ISchoolClassRepository
    {
        private readonly SchoolDbContext _context;

        public SchoolClassRepository(SchoolDbContext context) : base(context)
        {
            _context = context;
        }

        // Method to retrieve classes associated with a specific course
        public async Task<IEnumerable<SchoolClass>> GetClassesByCourseIdAsync(int courseId)
        {
            return await _context.SchoolClasses
                .Where(c => c.CourseId == courseId)
                .ToListAsync();
        }

        // Method to retrieve a class with its associated students
        public async Task<SchoolClass> GetClassWithStudentsAsync(int classId)
        {
            return await _context.SchoolClasses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classId);
        }

        // Method to retrieve a class with its associated teachers
        public async Task<SchoolClass> GetClassWithTeachersAsync(int classId)
        {
            return await _context.SchoolClasses
                .Include(c => c.TeacherSchoolClasses)
                .ThenInclude(tsc => tsc.Teacher)
                .FirstOrDefaultAsync(c => c.Id == classId);
        }

        // Method to check if a class is already assigned to a course
        public async Task<bool> IsClassAssignedToCourseAsync(int classId)
        {
            return await _context.SchoolClasses
                .AnyAsync(c => c.Id == classId && c.CourseId != null);
        }

        // Method to retrieve all classes
        public async Task<IEnumerable<SchoolClass>> GetAllAsync()
        {
            return await _context.SchoolClasses
                .Include(c => c.Students) // Include students for additional information if needed
                .ToListAsync();
        }

        // Method to retrieve all classes that are not associated with any course
        public async Task<IEnumerable<SchoolClass>> GetAllAvailableAsync()
        {
            return await _context.SchoolClasses
                .Where(sc => sc.CourseId == null)
                .ToListAsync();
        }
    }
}
