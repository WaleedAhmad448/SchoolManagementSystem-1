using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Subject : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }

        public string? Description { get; set; } // Descrição opcional

        public int Credits { get; set; } // Créditos que a disciplina vale

        // Coleção de professores associados a esta disciplina
        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();

        // Coleção de cursos associados a esta disciplina
        public ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>(); // Adicionada para associação
    }
}
