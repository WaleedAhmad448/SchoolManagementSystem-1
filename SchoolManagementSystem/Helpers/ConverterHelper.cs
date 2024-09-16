using System;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        // Conversão de StudentViewModel para Student
        public Student ToStudent(StudentViewModel model, Guid imageId, bool isNew)
        {
            return new Student
            {
                Id = isNew ? 0 : model.Id,
                UserId = model.UserId,
                SchoolClassId = model.SchoolClassId,
                EnrollmentDate = model.EnrollmentDate,
                Status = model.Status,
                ImageId = imageId
            };
        }

        // Conversão de Student para StudentViewModel
        public StudentViewModel ToStudentViewModel(Student student)
        {
            return new StudentViewModel
            {
                Id = student.Id,
                UserId = student.UserId,
                SchoolClassId = student.SchoolClassId,
                EnrollmentDate = student.EnrollmentDate,
                Status = student.Status,
                ImageId = student.ImageId
            };
        }

        // Conversão de TeacherViewModel para Teacher
        public Teacher ToTeacher(TeacherViewModel model, Guid imageId, bool isNew)
        {
            return new Teacher
            {
                Id = isNew ? 0 : model.Id,
                UserId = model.UserId,
                AcademicDegree = model.AcademicDegree,
                Department = model.Department,
                HireDate = model.HireDate,
                ImageId = imageId
            };
        }

        // Conversão de Teacher para TeacherViewModel
        public TeacherViewModel ToTeacherViewModel(Teacher teacher)
        {
            return new TeacherViewModel
            {
                Id = teacher.Id,
                UserId = teacher.UserId,
                AcademicDegree = teacher.AcademicDegree,
                Department = teacher.Department,
                HireDate = teacher.HireDate,
                ImageId = teacher.ImageId
            };
        }

        // Conversão de EmployeeViewModel para Employee
        public Employee ToEmployee(EmployeeViewModel model, Guid imageId, bool isNew)
        {
            return new Employee
            {
                Id = isNew ? 0 : model.Id,
                UserId = model.UserId,
                Department = model.Department,
                HireDate = model.HireDate,
                PhoneNumber = model.PhoneNumber,
                ImageId = imageId
            };
        }

        // Conversão de Employee para EmployeeViewModel
        public EmployeeViewModel ToEmployeeViewModel(Employee employee)
        {
            return new EmployeeViewModel
            {
                Id = employee.Id,
                UserId = employee.UserId,
                Department = employee.Department,
                HireDate = employee.HireDate,
                PhoneNumber = employee.PhoneNumber,
                ImageId = employee.ImageId
            };
        }
    }
}
