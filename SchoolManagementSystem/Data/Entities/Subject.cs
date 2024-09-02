using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SchoolManagementSystem.Data.Entities
{
    public class Subject : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }

        public string Description { get; set; }

        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
