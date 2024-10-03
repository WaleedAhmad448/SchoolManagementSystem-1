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

        var studentUser1 = await CreateUserAsync("student1@school.com", "Student1", "User", "Student123!", "Student");
        var studentUser2 = await CreateUserAsync("student2@school.com", "Student2", "User", "Student123!", "Student");

        // Criar cursos
        if (!_context.Courses.Any())
        {
            var course1 = new Course { CourseName = "Matemática", Description = "Curso de Matemática", Duration = 16, Credits = 5 };
            var course2 = new Course { CourseName = "Ciências", Description = "Curso de Ciências", Duration = 16, Credits = 5 };
            _context.Courses.AddRange(course1, course2);
            await _context.SaveChangesAsync(); // Guardar cursos
        }

        // Criar turmas
        if (!_context.SchoolClasses.Any())
        {
            var course1 = _context.Courses.FirstOrDefault(c => c.CourseName == "Matemática");
            var course2 = _context.Courses.FirstOrDefault(c => c.CourseName == "Ciências");

            // Adicionar a turma "No Class"
            var noClass = new SchoolClass
            {
                ClassName = "No Class", // Nome da turma
                CourseId = null, // Sem curso associado
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(6) // Defina as datas conforme necessário
            };

            var class1 = new SchoolClass { ClassName = "Turma A", CourseId = course1?.Id, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(6) };
            var class2 = new SchoolClass { ClassName = "Turma B", CourseId = course2?.Id, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddMonths(6) };

            _context.SchoolClasses.AddRange(noClass, class1, class2); // Adicione "No Class" aqui
            await _context.SaveChangesAsync(); // Guardar turmas
        }


        // Criar disciplinas
        if (!_context.Subjects.Any())
        {
            var class1 = _context.SchoolClasses.FirstOrDefault(c => c.ClassName == "Turma A");
            var class2 = _context.SchoolClasses.FirstOrDefault(c => c.ClassName == "Turma B");

            var subject1 = new Subject { SubjectName = "Álgebra", CourseId = class1?.CourseId, SchoolClassId = class1?.Id, StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1) };
            var subject2 = new Subject { SubjectName = "Biologia", CourseId = class2?.CourseId, SchoolClassId = class2?.Id, StartTime = DateTime.UtcNow, EndTime = DateTime.UtcNow.AddHours(1) };
            _context.Subjects.AddRange(subject1, subject2);
            await _context.SaveChangesAsync(); // Guardar disciplinas
        }

        // Criar estudantes com FirstName e LastName diretos
        if (!_context.Students.Any())
        {
            var class1 = _context.SchoolClasses.FirstOrDefault(c => c.ClassName == "Turma A");
            var class2 = _context.SchoolClasses.FirstOrDefault(c => c.ClassName == "Turma B");

            var student1 = new Student
            {
                FirstName = "Student1",
                LastName = "User",
                UserId = studentUser1.Id,
                EnrollmentDate = DateTime.UtcNow,
                Status = StudentStatus.Active,
                SchoolClassId = class1?.Id,
                ImageId = Guid.Empty // Imagem padrão
            };

            var student2 = new Student
            {
                FirstName = "Student2",
                LastName = "User",
                UserId = studentUser2.Id,
                EnrollmentDate = DateTime.UtcNow,
                Status = StudentStatus.Active,
                SchoolClassId = class2?.Id,
                ImageId = Guid.Empty // Imagem padrão
            };

            _context.Students.AddRange(student1, student2);
            await _context.SaveChangesAsync(); // Guardar estudantes
        }

        // Criar notas
        if (!_context.Grades.Any())
        {
            var student1 = _context.Students.FirstOrDefault(s => s.UserId == studentUser1.Id);
            var student2 = _context.Students.FirstOrDefault(s => s.UserId == studentUser2.Id);

            var subject1 = _context.Subjects.FirstOrDefault(s => s.SubjectName == "Álgebra");
            var subject2 = _context.Subjects.FirstOrDefault(s => s.SubjectName == "Biologia");

            var grade1 = new Grade { StudentId = student1?.Id, SubjectId = subject1?.Id, Value = 90, Status = "Passed", DateRecorded = DateTime.UtcNow };
            var grade2 = new Grade { StudentId = student2?.Id, SubjectId = subject2?.Id, Value = 85, Status = "Passed", DateRecorded = DateTime.UtcNow };
            _context.Grades.AddRange(grade1, grade2);
            await _context.SaveChangesAsync(); // Guardar notas
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
