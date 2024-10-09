using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class SubjectViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }

        public string? Description { get; set; } // Descrição opcional

        [Required]
        public int Credits { get; set; } // Créditos que a disciplina vale

        // Foreign keys
        public int? CourseId { get; set; } // FK opcional para associação a um curso
        public int? SchoolClassId { get; set; } // FK opcional para associação a uma turma

    }
}
