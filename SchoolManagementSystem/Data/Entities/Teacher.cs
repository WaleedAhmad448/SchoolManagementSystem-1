using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Teacher : IEntity
    {
        public int Id { get; set; }

        // Associação com o User
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [MaxLength(100)]
        public string AcademicDegree { get; set; } // Ex: Mestrado, Doutorado

        [MaxLength(100)]
        public string Department { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public ICollection<Subject> Subjects { get; set; } // Disciplinas ensinadas
    }
}
