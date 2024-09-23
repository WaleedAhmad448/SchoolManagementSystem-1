using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Presence")]
        public bool Presence { get; set; }

        public string Remarks { get; set; }

        public bool IsExcluded { get; set; }

        // Mensagem que exibe o estado de exclusão
        public string ExclusionMessage => IsExcluded
            ? "The student has been excluded due to excessive absences."
            : "The student is not excluded.";
    }
}
