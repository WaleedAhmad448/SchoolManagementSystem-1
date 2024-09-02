using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Class : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
