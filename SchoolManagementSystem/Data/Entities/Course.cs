using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Range(1, 52)]
        public int Duration { get; set; } // Duração em semanas, por exemplo.

        public int Credits { get; set; } // Créditos que o curso vale.

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }

        // Relacionamento: um curso pode ter várias turmas.
        public ICollection<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();

        // Relacionamento: um curso pode ter várias disciplinas associadas.
        public ICollection<CourseSubject> CourseSubjects { get; set; } = new List<CourseSubject>();
    }
}
