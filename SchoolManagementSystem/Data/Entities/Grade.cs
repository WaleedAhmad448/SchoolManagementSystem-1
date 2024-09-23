using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystem.Data.Entities
{
    public class Grade : IEntity
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }

        [Range(0, 100)]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Value { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        [Display(Name = "Date Recorded")]
        public DateTime DateRecorded { get; set; }
    }
}
