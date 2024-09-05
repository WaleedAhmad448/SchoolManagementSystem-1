using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<IEnumerable<Course>> GetCoursesByNameAsync(string courseName);
    Task<Course> GetCourseWithClassesAsync(int id);
    Task AddClassToCourseAsync(SchoolClass schoolClass, int courseId);
}
