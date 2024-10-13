using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Models;
using System;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Helpers
{
    public interface IConverterHelper
    {
        // Converts StudentViewModel to Student (async)
        Task<Student> ToStudentAsync(StudentViewModel model, Guid imageId, bool isNew);

        // Converts Student to StudentViewModel
        StudentViewModel ToStudentViewModel(Student student);

        // Converts TeacherViewModel to Teacher (async)
        Task<Teacher> ToTeacherAsync(TeacherViewModel model, Guid imageId, bool isNew);

        // Converts Teacher to TeacherViewModel
        TeacherViewModel ToTeacherViewModel(Teacher teacher);

        // Converts EmployeeViewModel to Employee (async)
        Task<Employee> ToEmployeeAsync(EmployeeViewModel model, Guid imageId, bool isNew);

        // Converts Employee to EmployeeViewModel
        EmployeeViewModel ToEmployeeViewModel(Employee employee);

        // Converts SchoolClassViewModel to SchoolClass (async)
        Task<SchoolClass> ToSchoolClassAsync(SchoolClassViewModel model, bool isNew);

        // Converts SchoolClass to SchoolClassViewModel
        SchoolClassViewModel ToSchoolClassViewModel(SchoolClass schoolClass);

        // Converts SubjectViewModel to Subject (async)
        Task<Subject> ToSubjectAsync(SubjectViewModel model);

        // Converts Subject to SubjectViewModel
        SubjectViewModel ToSubjectViewModel(Subject subject);

        // Converts CourseViewModel to Course (async)
        Task<Course> ToCourseAsync(CourseViewModel model, bool isNew);

        // Converts Course to CourseViewModel
        CourseViewModel ToCourseViewModel(Course course);
    }
}
