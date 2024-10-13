using System;
using NuGet.DependencyResolver;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Repositories;

namespace SchoolManagementSystem.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        // Converts the StudentViewModel to Student (entity)
        private readonly IUserHelper _userHelper;
        private readonly ISchoolClassRepository _schoolClassRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ICourseRepository _courseRepository;

        public ConverterHelper(IUserHelper userHelper, ISchoolClassRepository schoolClassRepository, ISubjectRepository subjectRepository, ICourseRepository courseRepository)
        {
            _userHelper = userHelper;
            _schoolClassRepository = schoolClassRepository;
            _subjectRepository = subjectRepository;
            _courseRepository = courseRepository;
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

        public async Task<Subject> ToSubjectAsync(SubjectViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            return new Subject
            {
                Id = model.Id,
                SubjectName = model.SubjectName,
                Description = model.Description,
                Credits = model.Credits,
            };
        }

        
        public SubjectViewModel ToSubjectViewModel(Subject subject)
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));

            return new SubjectViewModel
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName,
                Description = subject.Description,
                Credits = subject.Credits,
            };
        }

        public async Task<Course> ToCourseAsync(CourseViewModel model, bool isNew)
        {
            var course = new Course
            {
                
                CourseName = model.CourseName,
                Description = model.Description,
                Duration = model.Duration,
                Credits = model.Credits,
                IsActive = model.IsActive,
                CreatedAt = isNew ? DateTime.UtcNow : model.CreatedAt,
                UpdatedAt = DateTime.UtcNow
            };

            
            if (isNew)
            {
                
                await _courseRepository.CreateAsync(course);
            }
            else
            {
                course.Id = model.Id; 
                await _courseRepository.UpdateAsync(course);
            }

            
            if (model.SchoolClassIds != null && model.SchoolClassIds.Any())
            {
                foreach (var classId in model.SchoolClassIds)
                {
                    var schoolClass = await _schoolClassRepository.GetByIdAsync(classId);
                    if (schoolClass != null)
                    {
                        schoolClass.CourseId = course.Id; 
                        await _schoolClassRepository.UpdateAsync(schoolClass); 
                    }
                }
            }

            
            if (model.SubjectIds != null && model.SubjectIds.Any())
            {
                course.CourseSubjects = new List<CourseSubject>(); 
                foreach (var subjectId in model.SubjectIds)
                {
                    var subject = await _subjectRepository.GetByIdAsync(subjectId);
                    if (subject != null)
                    {
                        course.CourseSubjects.Add(new CourseSubject
                        {
                            CourseId = course.Id, 
                            SubjectId = subject.Id
                        });
                    }
                }
            }

            return course;
        }





        
        public CourseViewModel ToCourseViewModel(Course course)
        {
            return new CourseViewModel
            {
                Id = course.Id,
                CourseName = course.CourseName,
                Description = course.Description,
                Duration = course.Duration,
                Credits = course.Credits,
                IsActive = course.IsActive,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt,

                
                SchoolClassIds = course.SchoolClasses?.Select(sc => sc.Id).ToList() ?? new List<int>(),

                
                SubjectIds = course.CourseSubjects?.Select(cs => cs.SubjectId).ToList() ?? new List<int>()
            };
        }

        
        public async Task<SchoolClass> ToSchoolClassAsync(SchoolClassViewModel model, bool isNew)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            return new SchoolClass
            {
                Id = isNew ? 0 : model.Id, 
                ClassName = model.ClassName,
                CourseId = model.CourseId,
                StartDate = model.StartDate,
                EndDate = model.EndDate,

                
                Students = model.StudentIds.Select(id => new Student { Id = id }).ToList(),
                TeacherSchoolClasses = model.TeacherIds.Select(id => new TeacherSchoolClass { TeacherId = id }).ToList()
            };
        }

      
        public SchoolClassViewModel ToSchoolClassViewModel(SchoolClass schoolClass)
        {
            if (schoolClass == null) throw new ArgumentNullException(nameof(schoolClass));

            return new SchoolClassViewModel
            {
                Id = schoolClass.Id,
                ClassName = schoolClass.ClassName,
                CourseId = schoolClass.CourseId,
                StartDate = schoolClass.StartDate,
                EndDate = schoolClass.EndDate,

               
                StudentIds = schoolClass.Students.Select(s => s.Id).ToList(),
                TeacherIds = schoolClass.TeacherSchoolClasses.Select(t => t.TeacherId).ToList(),

                
                Students = schoolClass.Students,
                Teachers = schoolClass.TeacherSchoolClasses.Select(tsc => tsc.Teacher).ToList()
            };
        }



    }
}
