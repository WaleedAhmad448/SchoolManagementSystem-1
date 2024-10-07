using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Models;
using System;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Helpers
{
    public interface IConverterHelper
    {
        // Conversion from StudentViewModel to Student (asynchronous)
        Task<Student> ToStudentAsync(StudentViewModel model, Guid imageId, bool isNew);

        // Conversion from Student to StudentViewModel
        StudentViewModel ToStudentViewModel(Student student);

        // Conversion from TeacherViewModel to Teacher
        Task<Teacher> ToTeacherAsync(TeacherViewModel model, Guid imageId, bool isNew);

        // Conversion from Teacher to TeacherViewModel
        TeacherViewModel ToTeacherViewModel(Teacher teacher);

        // Conversion from EmployeeViewModel to Employee (asynchronous)
        Task<Employee> ToEmployeeAsync(EmployeeViewModel model, Guid imageId, bool isNew);

        // Conversion from Employee to EmployeeViewModel
        EmployeeViewModel ToEmployeeViewModel(Employee employee);
    }
}
