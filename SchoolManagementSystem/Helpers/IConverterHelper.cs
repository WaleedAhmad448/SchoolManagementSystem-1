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

        Task<Course> ToCourseAsync(CourseViewModel model); // Make sure this method is async
        CourseViewModel ToCourseViewModel(Course course);

        // Conversão SchoolClass
        Task<SchoolClass> ToSchoolClassAsync(SchoolClassViewModel model, bool isNew);
        SchoolClassViewModel ToSchoolClassViewModel(SchoolClass schoolClass);

        // Conversão de Subject
        Task<Subject> ToSubjectAsync(SubjectViewModel model); // Método para converter SubjectViewModel para Subject
        SubjectViewModel ToSubjectViewModel(Subject subject); // Método para converter Subject para SubjectViewModel
    }
}
