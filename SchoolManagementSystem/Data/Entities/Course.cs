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
        public int Duration { get; set; } // Duração em semanas

        public int Credits { get; set; }

        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}
