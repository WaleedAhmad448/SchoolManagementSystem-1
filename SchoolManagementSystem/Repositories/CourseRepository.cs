using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Repositories;

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
            .AsNoTracking()
            .Where(c => c.CourseName.Contains(courseName))
            .ToListAsync();
    }

    public async Task<Course> GetCourseWithClassesAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.SchoolClasses)
            .AsNoTracking() // Add AsNoTracking for performance
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddClassToCourseAsync(SchoolClass schoolClass, int courseId)
    {
        var course = await _context.Courses
            .Include(c => c.SchoolClasses)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with ID {courseId} not found.");
        }

        // Ensure that the class is not already associated
        if (!course.SchoolClasses.Any(sc => sc.Id == schoolClass.Id))
        {
            course.SchoolClasses.Add(schoolClass);
            await _context.SaveChangesAsync(); // Save changes
        }
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _context.Courses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetCoursesWithSubjectsAsync()
    {
        return await _context.Courses
            .Include(c => c.Subjects)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task RemoveSubjectFromCourseAsync(int courseId, int subjectId)
    {
        var course = await _context.Courses
            .Include(c => c.Subjects)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with ID {courseId} not found.");
        }

        var subjectToRemove = course.Subjects.FirstOrDefault(s => s.Id == subjectId);
        if (subjectToRemove != null)
        {
            course.Subjects.Remove(subjectToRemove);
            await _context.SaveChangesAsync(); // Save changes
        }
        else
        {
            throw new InvalidOperationException($"Subject with ID {subjectId} not found in Course with ID {courseId}.");
        }
    }

    public async Task<int> CountCoursesAsync()
    {
        return await _context.Courses.CountAsync();
    }
}
