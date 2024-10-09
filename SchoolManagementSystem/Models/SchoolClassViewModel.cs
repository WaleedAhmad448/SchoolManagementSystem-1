using SchoolManagementSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class SchoolClassViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Class Name is required")]
        [Display(Name = "Class Name")]
        [MaxLength(50, ErrorMessage = "Class Name cannot exceed 50 characters")]
        public string ClassName { get; set; }

        // Curso associado (pode ser opcional)
        [Display(Name = "Associated Course")]
        public int? CourseId { get; set; } // Mantemos como nullable para permitir "No Class"

        public Course? Course { get; set; } // Curso relacionado (opcional)

        // Data de início e fim da turma
        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        // IDs de disciplinas associadas
        [Display(Name = "Associated Subjects")]
        public ICollection<int>? SubjectIds { get; set; } = new List<int>();

        // IDs de alunos associados
        [Display(Name = "Associated Students")]
        public ICollection<int>? StudentIds { get; set; } = new List<int>();

        // Lista para popular os dropdowns ou checkboxes nas views
        public IEnumerable<Course>? Courses { get; set; } // Lista de cursos para selecionar
        public IEnumerable<Subject>? Subjects { get; set; } // Lista de disciplinas para selecionar
        public IEnumerable<Student>? Students { get; set; } // Lista de alunos para selecionar
    }
}
