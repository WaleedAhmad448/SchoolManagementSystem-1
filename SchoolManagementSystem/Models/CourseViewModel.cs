using SchoolManagementSystem.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Name is required")]
        [Display(Name = "Course Name")]
        [MaxLength(100, ErrorMessage = "Course Name cannot exceed 100 characters")]
        public string CourseName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 52, ErrorMessage = "Duration must be between 1 and 52 weeks")]
        [Display(Name = "Duration (weeks)")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Credits are required")]
        [Display(Name = "Credits")]
        public int Credits { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        // IDs for the associated subjects and school classes
        [Display(Name = "Associated Subjects")]
        public ICollection<int> SubjectIds { get; set; } = new List<int>();

        [Display(Name = "Associated Classes")]
        public ICollection<int> SchoolClassIds { get; set; } = new List<int>();

        // Data for populating dropdowns
        public IEnumerable<Subject> Subjects { get; set; } = new List<Subject>(); // Initializing to avoid null
        public IEnumerable<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>(); // Initializing to avoid null
    }
}
