using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Repositories
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        // Searches for courses by name
        Task<IEnumerable<Course>> GetCoursesByNameAsync(string courseName);

        // Retrieves all courses without tracking
        Task<IEnumerable<Course>> GetAllCoursesAsync();

        // Gets a course with its associated classes
        Task<Course> GetCourseWithClassesAsync(int id);

        // Retrieves courses with their associated subjects
        Task<IEnumerable<Course>> GetCoursesWithSubjectsAsync();

        // Adds a class to the specified course
        Task AddClassToCourseAsync(SchoolClass schoolClass, int courseId);

        // Removes a subject from a course
        Task RemoveSubjectFromCourseAsync(int courseId, int subjectId);

        // Checks if a subject is already associated with a course
        Task<bool> IsSubjectInCourseAsync(int courseId, int subjectId);

        // Counts the total number of courses
        Task<int> CountCoursesAsync();

        // Gets a course with its classes and subjects
        Task<Course> GetCourseWithClassesAndSubjectsAsync(int id);

        // Retrieves all available subjects
        Task<IEnumerable<Subject>> GetAllAvailableSubjectsAsync();

        // Retrieves all available school classes
        Task<IEnumerable<SchoolClass>> GetAllAvailableSchoolClassesAsync();
    }
}
