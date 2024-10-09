using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }

        public string Description { get; set; }

        [Range(1, 52)]
        public int Duration { get; set; }

        public int Credits { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }

        // Coleção de turmas associadas
        public ICollection<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();

        // Coleção de disciplinas associadas
        public ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>(); // Adicionada para associação

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    }
}
