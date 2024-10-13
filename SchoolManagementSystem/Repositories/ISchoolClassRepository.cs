using SchoolManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Repositories
{
    public interface ISchoolClassRepository : IGenericRepository<SchoolClass>
    {
        Task<IEnumerable<SchoolClass>> GetClassesByCourseIdAsync(int courseId);
        Task<SchoolClass> GetClassWithStudentsAsync(int classId);
        Task<SchoolClass> GetClassWithTeachersAsync(int classId);
        Task<bool> IsClassAssignedToCourseAsync(int classId);
        Task<IEnumerable<SchoolClass>> GetAllAsync();
        Task<IEnumerable<SchoolClass>> GetAllAvailableAsync();
    }
}
