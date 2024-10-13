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

    // Retrieves courses by name (case-insensitive)
    public async Task<IEnumerable<Course>> GetCoursesByNameAsync(string courseName)
    {
        return await _context.Courses
            .AsNoTracking()
            .Where(c => c.CourseName.ToLower().Contains(courseName.ToLower()))
            .ToListAsync();
    }

    // Gets a course with its associated classes
    public async Task<Course> GetCourseWithClassesAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.SchoolClasses)
            .FirstOrDefaultAsync(c => c.Id == id); // Keep tracking for potential edits
    }

    // Gets all courses without tracking
    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _context.Courses
            .AsNoTracking()
            .ToListAsync();
    }

    // Gets courses with their associated subjects
    public async Task<IEnumerable<Course>> GetCoursesWithSubjectsAsync()
    {
        return await _context.Courses
            .Include(c => c.CourseSubjects)
            .ThenInclude(cs => cs.Subject) // Includes subjects through the CourseSubject join table
            .AsNoTracking()
            .ToListAsync();
    }

    // Adds a class to a course
    public async Task AddClassToCourseAsync(SchoolClass schoolClass, int courseId)
    {
        var course = await _context.Courses
            .Include(c => c.SchoolClasses) // Includes the SchoolClasses collection
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with ID {courseId} not found.");
        }

        // Checks if the class is already associated with the course
        if (!course.SchoolClasses.Any(sc => sc.Id == schoolClass.Id))
        {
            var classToAdd = await _context.SchoolClasses.FirstOrDefaultAsync(sc => sc.Id == schoolClass.Id);
            if (classToAdd != null)
            {
                course.SchoolClasses.Add(classToAdd); // Adds the class to the course
                await _context.SaveChangesAsync(); // Saves changes
            }
        }
    }

    // Removes a subject from a course
    public async Task RemoveSubjectFromCourseAsync(int courseId, int subjectId)
    {
        var course = await _context.Courses
            .Include(c => c.CourseSubjects) // Includes the CourseSubjects join table
            .ThenInclude(cs => cs.Subject)  // Includes the Subject entity
            .FirstOrDefaultAsync(c => c.Id == courseId);

        if (course == null)
        {
            throw new KeyNotFoundException($"Course with ID {courseId} not found.");
        }

        var subjectToRemove = course.CourseSubjects.FirstOrDefault(cs => cs.SubjectId == subjectId);
        if (subjectToRemove != null)
        {
            course.CourseSubjects.Remove(subjectToRemove); // Removes the relationship
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"Subject with ID {subjectId} not found in Course with ID {courseId}.");
        }
    }

    // Counts the total number of courses
    public async Task<int> CountCoursesAsync()
    {
        return await _context.Courses.CountAsync();
    }

    // Checks if a subject is already associated with a course
    public async Task<bool> IsSubjectInCourseAsync(int courseId, int subjectId)
    {
        return await _context.CourseSubjects
            .AnyAsync(cs => cs.CourseId == courseId && cs.SubjectId == subjectId);
    }

    // Gets a course with its associated classes and subjects
    public async Task<Course> GetCourseWithClassesAndSubjectsAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.SchoolClasses)
            .Include(c => c.CourseSubjects)
            .ThenInclude(cs => cs.Subject)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    // Gets all subjects not associated with any course
    public async Task<IEnumerable<Subject>> GetAllAvailableSubjectsAsync()
    {
        return await _context.Subjects
            .Where(s => !_context.CourseSubjects.Any(cs => cs.SubjectId == s.Id))
            .ToListAsync();
    }

    // Gets all classes not associated with any course
    public async Task<IEnumerable<SchoolClass>> GetAllAvailableSchoolClassesAsync()
    {
        return await _context.SchoolClasses
            .Where(sc => sc.CourseId == null)
            .ToListAsync();
    }
}
