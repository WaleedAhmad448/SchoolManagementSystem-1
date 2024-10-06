using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class SchoolClass : IEntity
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string ClassName { get; set; }

        public int? CourseId { get; set; } // Chave estrangeira nullable
        public Course? Course { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public ICollection<Student>? Students { get; set; } // Coleção de alunos

        public ICollection<Subject>? Subjects { get; set; } // Coleção de disciplinas

        public ICollection<TeacherSchoolClass>? TeacherSchoolClasses { get; set; } // Coleção de professores
    }
}
