using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    private readonly SchoolDbContext _context;

    public CourseRepository(SchoolDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetCoursesByNameAsync(string courseName)
    {
        return await _context.Courses
            .Where(c => c.CourseName.Contains(courseName))
            .ToListAsync();
    }

    public async Task<Course> GetCourseWithClassesAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.SchoolClasses)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddClassToCourseAsync(SchoolClass schoolClass, int courseId)
    {
        var course = await GetCourseWithClassesAsync(courseId);
        if (course == null)
        {
            return;
        }

        course.SchoolClasses.Add(schoolClass);
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }
}
