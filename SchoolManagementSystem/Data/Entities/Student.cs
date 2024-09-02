using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SchoolManagementSystem.Data.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } // Ex: Ativo, Inativo

    }
}
