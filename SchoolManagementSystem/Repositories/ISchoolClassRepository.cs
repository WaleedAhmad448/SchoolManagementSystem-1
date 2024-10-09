using SchoolManagementSystem.Data.Entities;

namespace SchoolManagementSystem.Repositories
{
    public interface ISchoolClassRepository : IGenericRepository<SchoolClass>
    {
        /// <summary>
        /// Retrieves all school classes associated with a specific course.
        /// </summary>
        /// <param name="courseId">The ID of the course.</param>
        /// <returns>A collection of school classes.</returns>
        Task<IEnumerable<SchoolClass>> GetClassesByCourseIdAsync(int courseId);

        /// <summary>
        /// Counts the number of students enrolled in a specific school class.
        /// </summary>
        /// <param name="classId">The ID of the school class.</param>
        /// <returns>The number of students in the class.</returns>
        Task<int> GetStudentCountByClassIdAsync(int classId);

        /// <summary>
        /// Retrieves a specific school class along with its associated subjects.
        /// </summary>
        /// <param name="classId">The ID of the school class.</param>
        /// <returns>The school class with its subjects.</returns>
        Task<SchoolClass> GetClassWithSubjectsAsync(int classId);

        /// <summary>
        /// Retrieves all school classes from the database.
        /// </summary>
        /// <returns>A collection of all school classes.</returns>
        Task<IEnumerable<SchoolClass>> GetAllAsync();

        /// <summary>
        /// Retrieves school classes that are not associated with any course.
        /// </summary>
        /// <returns>A collection of school classes without associated courses.</returns>
        Task<IEnumerable<SchoolClass>> GetClassesWithoutCourseAsync();

        /// <summary>
        /// Retrieves a specific school class with its associated teachers.
        /// </summary>
        /// <param name="classId">The ID of the school class.</param>
        /// <returns>The school class with its teachers.</returns>
        Task<SchoolClass> GetClassWithTeachersAsync(int classId);

        /// <summary>
        /// Retrieves a specific school class with its associated students.
        /// </summary>
        /// <param name="classId">The ID of the school class.</param>
        /// <returns>The school class with its students.</returns>
        Task<SchoolClass> GetClassWithStudentsAsync(int classId);
    }
}
