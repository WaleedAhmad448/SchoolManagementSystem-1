using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICourseRepository : IGenericRepository<Course>
{
    // Get all courses that contain the specified name
    Task<IEnumerable<Course>> GetCoursesByNameAsync(string courseName);

    // Get a course along with its associated classes
    Task<Course> GetCourseWithClassesAsync(int id);

    // Add a class to the specified course
    Task AddClassToCourseAsync(SchoolClass schoolClass, int courseId);

    // Get all courses
    Task<IEnumerable<Course>> GetAllCoursesAsync();

    // Get all courses with their associated subjects
    Task<IEnumerable<Course>> GetCoursesWithSubjectsAsync();

    // Remove a subject from a specified course
    Task RemoveSubjectFromCourseAsync(int courseId, int subjectId);

    // Count the total number of courses
    Task<int> CountCoursesAsync();
}
