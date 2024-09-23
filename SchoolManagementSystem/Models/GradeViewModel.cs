using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class GradeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Range(0, 100)]
        public decimal Value { get; set; }

        public string Status { get; set; }

        [Display(Name = "Date Recorded")]
        public DateTime DateRecorded { get; set; }
    }
}
