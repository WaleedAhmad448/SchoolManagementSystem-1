using System;
using SchoolManagementSystem.Data.Entities;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        // Converte o StudentViewModel em Student (entidade)
        private readonly IUserHelper _userHelper;

        public ConverterHelper(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }


        // Converte o StudentViewModel em Student (entidade)
        public async Task<Student> ToStudentAsync(StudentViewModel model, Guid imageId, bool isNew)
        {
            // Busca o utilizador com o role "Pending" selecionado na dropdown
            var user = await _userHelper.GetUserByIdAsync(model.UserId);
            if (user == null) throw new Exception("User not found");

            return new Student
            {
                Id = isNew ? 0 : model.Id, // Se for novo, define o Id como 0
                UserId = user.Id, // Usa o UserId correto
                FirstName = model.FirstName, // Utiliza o FirstName do ViewModel
                LastName = model.LastName, // Utiliza o LastName do ViewModel
                EnrollmentDate = model.EnrollmentDate, // Usa a data de matrícula do ViewModel
                Status = model.Status, // Converte o status diretamente do ViewModel
                SchoolClassId = model.SchoolClassId, // Relaciona o aluno à turma a partir do ViewModel
                SchoolClass = model.SchoolClass,
                ImageId = imageId // Usa o ID da imagem, que foi gerado ao fazer upload (ou o que já existia)
            };
        }

        // Converte o Student (entidade) para StudentViewModel
        public StudentViewModel ToStudentViewModel(Student student)
        {
            return new StudentViewModel
            {
                Id = student.Id,
                UserId = student.UserId, // Mapeia corretamente o UserId do estudante
                FirstName = student.FirstName, // Mapeia o FirstName do estudante
                LastName = student.LastName, // Mapeia o LastName do estudante
                EnrollmentDate = student.EnrollmentDate, // Data de matrícula
                Status = student.Status, // Converte o status enum
                SchoolClassId = student.SchoolClassId,
                SchoolClass = student.SchoolClass,// Relaciona com a turma
                ImageId = student.ImageId // Mantém o ImageId atual no ViewModel
            };
        }


        // Conversão de TeacherViewModel para Teacher
        public async Task<Teacher> ToTeacherAsync(TeacherViewModel model, Guid imageId, bool isNew)
        {
            // Busca o utilizador relacionado ao Teacher
            var user = await _userHelper.GetUserByIdAsync(model.UserId);
            if (user == null) throw new Exception("User not found");

            return new Teacher
            {
                Id = isNew ? 0 : model.Id,
                UserId = user.Id, // Usa o UserId correto
                FirstName = model.FirstName,
                LastName = model.LastName,
                AcademicDegree = model.AcademicDegree,
                HireDate = model.HireDate,
                Status = model.Status,
                ImageId = imageId,

                // Criando as associações para TeacherSchoolClass e TeacherSubject
                TeacherSchoolClasses = model.SchoolClassIds.Select(id => new TeacherSchoolClass
                {
                    SchoolClassId = id // Aqui, deve usar a chave da tabela de junção se tiver
                }).ToList(),

                TeacherSubjects = model.SubjectIds.Select(subjectId => new TeacherSubject
                {
                    TeacherId = isNew ? 0 : model.Id, // O ID do Teacher deve ser definido
                    SubjectId = subjectId
                }).ToList()
            };
        }

        // Conversão de Teacher para TeacherViewModel
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

                // Coletando os IDs das turmas e disciplinas
                SchoolClassIds = teacher.TeacherSchoolClasses.Select(tsc => tsc.SchoolClassId).ToList(),
                SubjectIds = teacher.TeacherSubjects.Select(ts => ts.SubjectId).ToList(),

                // Coletando as instâncias completas para exibição
                SchoolClasses = teacher.TeacherSchoolClasses.Select(tsc => tsc.SchoolClass).ToList(),
                Subjects = teacher.TeacherSubjects.Select(ts => ts.Subject).ToList()
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
