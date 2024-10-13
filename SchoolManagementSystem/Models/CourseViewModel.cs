using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1, 52, ErrorMessage = "The course duration must be between 1 and 52 weeks.")]
        public int Duration { get; set; } 

        [Required]
        public int Credits { get; set; } 

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }

        
        public List<int> SchoolClassIds { get; set; } = new List<int>(); 
        public List<int> SubjectIds { get; set; } = new List<int>(); 

        
        public IEnumerable<SelectListItem> AvailableSchoolClasses { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> AvailableSubjects { get; set; } = new List<SelectListItem>();

        
        public IEnumerable<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>(); 
        public IEnumerable<Subject> Subjects { get; set; } = new List<Subject>(); 
    }
}
