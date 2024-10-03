using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Attendance : IEntity
    {
        public int Id { get; set; } // This will be the primary key, as required by the IEntity interface


        public int StudentId { get; set; }

        // Navigation to the Student entity
        public Student Student { get; set; }


        public int SubjectId { get; set; }

        // Navigation to the Subject entity
        public Subject Subject { get; set; }

        // Class date
        [Required]
        public DateTime Date { get; set; }

        // Presence: Present or Absent
        public bool Presence { get; set; }

        // Additional comments (optional)
        public string Remarks { get; set; }
    
    }
}
