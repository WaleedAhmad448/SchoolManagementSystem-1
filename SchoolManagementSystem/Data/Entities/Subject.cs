using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Subject : IEntity
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string SubjectName { get; set; }

        public string? Description { get; set; } // Descrição opcional

        public int? CourseId { get; set; } // Chave estrangeira nullable
        public Course? Course { get; set; }

        public int? TeacherId { get; set; } // Chave estrangeira nullable
        public Teacher? Teacher { get; set; }

        public int? SchoolClassId { get; set; } // Chave estrangeira nullable
        public SchoolClass? SchoolClass { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
