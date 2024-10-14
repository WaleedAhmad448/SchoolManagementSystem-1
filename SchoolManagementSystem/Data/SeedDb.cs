using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

public class SeedDb
{
    private readonly SchoolDbContext _context;
    private readonly IUserHelper _userHelper;

    public SeedDb(SchoolDbContext context, IUserHelper userHelper)
    {
        _context = context;
        _userHelper = userHelper;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();

        // Criar roles e utilizadores
        await _userHelper.CheckRoleAsync("Admin");
        await _userHelper.CheckRoleAsync("Student");
        await _userHelper.CheckRoleAsync("Teacher");
        await _userHelper.CheckRoleAsync("Employee");

        var studentUser1 = await CreateUserAsync("student1@school.com", "Student1", "User", "Student123!", "Student");
        var studentUser2 = await CreateUserAsync("student2@school.com", "Student2", "User", "Student123!", "Student");

        //    // Criar cursos
        //    if (!_context.Courses.Any())
        //    {
        //        var course1 = new Course
        //        {
        //            Name = "Mathematics",
        //            Description = "Mathematics Course",
        //            Duration = 16,
        //            IsActive = true,
        //            CreatedAt = DateTime.UtcNow,
        //            UpdatedAt = DateTime.UtcNow
        //        };

        //        var course2 = new Course
        //        {
        //            Name = "Science",
        //            Description = "Science Course",
        //            Duration = 16,
        //            IsActive = true,
        //            CreatedAt = DateTime.UtcNow,
        //            UpdatedAt = DateTime.UtcNow
        //        };

        //        _context.Courses.AddRange(course1, course2);
        //        await _context.SaveChangesAsync(); // Salvar cursos
        //    }

        //    // Criar turmas após garantir que os cursos existem
        //    if (!_context.SchoolClasses.Any())
        //    {
        //        var course1 = _context.Courses.FirstOrDefault(c => c.Name == "Mathematics");
        //        var course2 = _context.Courses.FirstOrDefault(c => c.Name == "Science");

        //        var class1 = new SchoolClass
        //        {
        //            ClassName = "Class A",
        //            CourseId = course1.Id, // Associa com o curso "Mathematics"
        //            StartDate = DateTime.UtcNow,
        //            EndDate = DateTime.UtcNow.AddMonths(6)
        //        };

        //        var class2 = new SchoolClass
        //        {
        //            ClassName = "Class B",
        //            CourseId = null, // Esta turma não está associada a um curso neste momento
        //            StartDate = DateTime.UtcNow,
        //            EndDate = DateTime.UtcNow.AddMonths(6)
        //        };

        //        var noClass = new SchoolClass
        //        {
        //            ClassName = "No Class",
        //            CourseId = null, // Sem curso associado
        //            StartDate = DateTime.UtcNow,
        //            EndDate = DateTime.UtcNow.AddMonths(6)
        //        };

        //        _context.SchoolClasses.AddRange(class1, class2, noClass);
        //        await _context.SaveChangesAsync(); // Salvar turmas
        //    }

        //    // Criar disciplinas
        //    if (!_context.Subjects.Any())
        //    {
        //        var subject1 = new Subject
        //        {
        //            Name = "Mathematics",
        //            Description = "Basic Mathematics",
        //        };

        //        var subject2 = new Subject
        //        {
        //            Name = "Science",
        //            Description = "Basic Science",

        //        };

        //        var subject3 = new Subject
        //        {
        //            Name = "No Subject",
        //            Description = "No specific subject assigned.",

        //        };

        //        _context.Subjects.AddRange(subject1, subject2, subject3);
        //        await _context.SaveChangesAsync(); // Salvar disciplinas
        //    }

        //    // Criar estudantes
        //    if (!_context.Students.Any())
        //    {
        //        var class1 = _context.SchoolClasses.FirstOrDefault(c => c.ClassName == "Class A");
        //        var class2 = _context.SchoolClasses.FirstOrDefault(c => c.ClassName == "Class B");
        //        var noClass = _context.SchoolClasses.FirstOrDefault(c => c.ClassName == "No Class");

        //        var student1 = new Student
        //        {
        //            FirstName = "Student1",
        //            LastName = "User",
        //            UserId = studentUser1.Id,
        //            EnrollmentDate = DateTime.UtcNow,
        //            Status = StudentStatus.Active,
        //            SchoolClassId = class1?.Id ?? noClass?.Id, // Se Class A não existir, usa No Class
        //            ImageId = Guid.Empty
        //        };

        //        var student2 = new Student
        //        {
        //            FirstName = "Student2",
        //            LastName = "User",
        //            UserId = studentUser2.Id,
        //            EnrollmentDate = DateTime.UtcNow,
        //            Status = StudentStatus.Active,
        //            SchoolClassId = class2?.Id ?? noClass?.Id, // Se Class B não existir, usa No Class
        //            ImageId = Guid.Empty
        //        };

        //        _context.Students.AddRange(student1, student2);
        //        await _context.SaveChangesAsync(); // Salvar estudantes
        //    }

        //    // Criar professores
        //    if (!_context.Teachers.Any())
        //    {
        //        // Criar um novo usuário para o professor
        //        var teacherUser = await CreateUserAsync("teacher1@school.com", "Teacher1", "User", "Teacher123!", "Teacher");

        //        // Obter as turmas
        //        var class1 = await _context.SchoolClasses.FirstOrDefaultAsync(c => c.ClassName == "Class A");
        //        var class2 = await _context.SchoolClasses.FirstOrDefaultAsync(c => c.ClassName == "Class B");

        //        // Verificar se as turmas existem
        //        if (class1 == null || class2 == null)
        //        {
        //            throw new InvalidOperationException("Classes must be created before associating teachers to them.");
        //        }

        //        // Obter as disciplinas
        //        var subject1 = await _context.Subjects.FirstOrDefaultAsync(s => s.Name == "Mathematics");
        //        var subject2 = await _context.Subjects.FirstOrDefaultAsync(s => s.Name == "Science");

        //        // Criar um novo professor
        //        var teacher = new Teacher
        //        {
        //            FirstName = "Teacher1",
        //            LastName = "User",
        //            UserId = teacherUser.Id,
        //            HireDate = DateTime.UtcNow,
        //            Status = TeacherStatus.Active,
        //            AcademicDegree = AcademicDegree.BachelorsDegree,
        //            ImageId = Guid.Empty // Defina a lógica apropriada para ImageId, se necessário
        //        };

        //        // Adicionar o professor ao contexto
        //        await _context.Teachers.AddAsync(teacher);
        //        await _context.SaveChangesAsync(); // Salvar o novo professor

        //        // Associar o professor a turmas
        //        var teacherSchoolClasses = new[]
        //        {
        //    new TeacherSchoolClass { TeacherId = teacher.Id, SchoolClassId = class1.Id },
        //    new TeacherSchoolClass { TeacherId = teacher.Id, SchoolClassId = class2.Id }
        //};

        //        await _context.TeacherSchoolClasses.AddRangeAsync(teacherSchoolClasses);

        //        // Associar o professor a disciplinas
        //        var teacherSubjects = new[]
        //        {
        //    new TeacherSubject { TeacherId = teacher.Id, SubjectId = subject1.Id },
        //    new TeacherSubject { TeacherId = teacher.Id, SubjectId = subject2.Id }
        //};

        //        await _context.TeacherSubjects.AddRangeAsync(teacherSubjects);

        //        await _context.SaveChangesAsync(); // Salvar as associações
        //    }


        // Criar funcionários
        if (!_context.Employees.Any())
        {
            var employeeUser1 = await CreateUserAsync("employee1@school.com", "Employee1", "User", "Employee123!", "Employee");
            var employeeUser2 = await CreateUserAsync("employee2@school.com", "Employee2", "User", "Employee123!", "Employee");

            var employee1 = new Employee
            {
                FirstName = "Employee1",
                LastName = "User",
                UserId = employeeUser1.Id,
                Department = Department.Administration,
                HireDate = DateTime.UtcNow,
                PhoneNumber = "123-456-7890",
                Status = EmployeeStatus.Active,
                ImageId = Guid.Empty
            };

            var employee2 = new Employee
            {
                FirstName = "Employee2",
                LastName = "User",
                UserId = employeeUser2.Id,
                Department = Department.HumanResources,
                HireDate = DateTime.UtcNow,
                PhoneNumber = "987-654-3210",
                Status = EmployeeStatus.Active,
                ImageId = Guid.Empty
            };

            _context.Employees.AddRange(employee1, employee2);
            await _context.SaveChangesAsync(); // Salvar funcionários
        }
    }

    private async Task<User> CreateUserAsync(string email, string firstName, string lastName, string password, string role)
    {
        var user = await _userHelper.GetUserByEmailAsync(email);
        if (user == null)
        {
            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                DateCreated = DateTime.UtcNow
            };

            await _userHelper.AddUserAsync(user, password);
            await _userHelper.AddUserToRoleAsync(user, role);
        }
        return user;
    }
}
