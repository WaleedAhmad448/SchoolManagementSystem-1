using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Models;
using System;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Helpers
{
    public interface IConverterHelper
    {
        // Conversão de StudentViewModel para Student (assíncrono)
        Task<Student> ToStudentAsync(StudentViewModel model, Guid imageId, bool isNew);

        // Conversão de Student para StudentViewModel
        StudentViewModel ToStudentViewModel(Student student);

        // Conversão de TeacherViewModel para Teacher
        Teacher ToTeacher(TeacherViewModel model, Guid imageId, bool isNew);

        // Conversão de Teacher para TeacherViewModel
        TeacherViewModel ToTeacherViewModel(Teacher teacher);

        // Conversão de EmployeeViewModel para Employee
        Employee ToEmployee(EmployeeViewModel model, Guid imageId, bool isNew);

        // Conversão de Employee para EmployeeViewModel
        EmployeeViewModel ToEmployeeViewModel(Employee employee);
    }
}
