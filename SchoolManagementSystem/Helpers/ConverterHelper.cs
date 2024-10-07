using System;
using NuGet.DependencyResolver;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        // Converts the StudentViewModel to Student (entity)
        private readonly IUserHelper _userHelper;

        public ConverterHelper(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        // Converts the StudentViewModel to Student (entity)
        public async Task<Student> ToStudentAsync(StudentViewModel model, Guid imageId, bool isNew)
        {
            // Fetches the user with the "Pending" role selected in the dropdown
            var user = await _userHelper.GetUserByIdAsync(model.UserId);
            if (user == null) throw new Exception("User not found");

            return new Student
            {
                Id = isNew ? 0 : model.Id, // If new, sets Id to 0
                UserId = user.Id, // Uses the correct UserId
                FirstName = model.FirstName, // Uses FirstName from the ViewModel
                LastName = model.LastName, // Uses LastName from the ViewModel
                EnrollmentDate = model.EnrollmentDate, // Uses the enrollment date from the ViewModel
                Status = model.Status, // Converts the status directly from the ViewModel
                SchoolClassId = model.SchoolClassId, // Relates the student to the class from the ViewModel
                SchoolClass = model.SchoolClass,
                ImageId = imageId // Uses the image ID generated during upload (or the existing one)
            };
        }

        // Converts the Student (entity) to StudentViewModel
        public StudentViewModel ToStudentViewModel(Student student)
        {
            return new StudentViewModel
            {
                Id = student.Id,
                UserId = student.UserId, // Correctly maps the student's UserId
                FirstName = student.FirstName, // Maps the student's FirstName
                LastName = student.LastName, // Maps the student's LastName
                EnrollmentDate = student.EnrollmentDate, // Enrollment date
                Status = student.Status, // Converts the status enum
                SchoolClassId = student.SchoolClassId,
                SchoolClass = student.SchoolClass, // Relates to the class
                ImageId = student.ImageId // Keeps the current ImageId in the ViewModel
            };
        }

        // Conversion from TeacherViewModel to Teacher
        public async Task<Teacher> ToTeacherAsync(TeacherViewModel model, Guid imageId, bool isNew)
        {
            // Fetches the user related to the Teacher
            var user = await _userHelper.GetUserByIdAsync(model.UserId);
            if (user == null) throw new Exception("User not found");

            return new Teacher
            {
                Id = isNew ? 0 : model.Id,
                UserId = user.Id, // Uses the correct UserId
                FirstName = model.FirstName,
                LastName = model.LastName,
                AcademicDegree = model.AcademicDegree,
                HireDate = model.HireDate,
                Status = model.Status,
                ImageId = imageId,

                // Creating associations for TeacherSchoolClass and TeacherSubject
                TeacherSchoolClasses = model.SchoolClassIds.Select(id => new TeacherSchoolClass
                {
                    SchoolClassId = id // Here, should use the key from the junction table if it has one
                }).ToList(),

                TeacherSubjects = model.SubjectIds.Select(subjectId => new TeacherSubject
                {
                    TeacherId = isNew ? 0 : model.Id, // The Teacher's ID must be set
                    SubjectId = subjectId
                }).ToList()
            };
        }

        // Conversion from Teacher to TeacherViewModel
        public TeacherViewModel ToTeacherViewModel(Teacher teacher)
        {
            return new TeacherViewModel
            {
                Id = teacher.Id,
                UserId = teacher.UserId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                AcademicDegree = teacher.AcademicDegree,
                HireDate = teacher.HireDate,
                ImageId = teacher.ImageId,
                Status = teacher.Status,

                // Collecting the IDs of classes and subjects
                SchoolClassIds = teacher.TeacherSchoolClasses.Select(tsc => tsc.SchoolClassId).ToList(),
                SubjectIds = teacher.TeacherSubjects.Select(ts => ts.SubjectId).ToList(),

                // Collecting the complete instances for display
                SchoolClasses = teacher.TeacherSchoolClasses.Select(tsc => tsc.SchoolClass).ToList(),
                Subjects = teacher.TeacherSubjects.Select(ts => ts.Subject).ToList()
            };
        }

        // Conversion from EmployeeViewModel to Employee
        public async Task<Employee> ToEmployeeAsync(EmployeeViewModel model, Guid imageId, bool isNew)
        {
            var user = await _userHelper.GetUserByIdAsync(model.UserId);
            if (user == null) throw new Exception("User not found");

            return new Employee
            {
                Id = isNew ? 0 : model.Id,
                UserId = user.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Department = model.Department,
                AcademicDegree = model.AcademicDegree,
                HireDate = model.HireDate,
                PhoneNumber = model.PhoneNumber,
                ImageId = imageId,
                Status = model.Status
            };
        }

        // Conversion from Employee to EmployeeViewModel
        public EmployeeViewModel ToEmployeeViewModel(Employee employee)
        {
            return new EmployeeViewModel
            {
                Id = employee.Id,
                UserId = employee.UserId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Department = employee.Department,
                AcademicDegree = employee.AcademicDegree,
                HireDate = employee.HireDate,
                PhoneNumber = employee.PhoneNumber,
                ImageId = employee.ImageId,
                Status = employee.Status
            };
        }
    }
}
