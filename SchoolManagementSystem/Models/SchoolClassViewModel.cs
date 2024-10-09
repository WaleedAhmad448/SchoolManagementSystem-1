using SchoolManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class SchoolClassViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        public int? CourseId { get; set; } // FK para o curso associado

        [Display(Name = "Start Date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required]
        public DateTime EndDate { get; set; }

        // Lista de IDs de alunos associados a esta turma
        public List<int> StudentIds { get; set; } = new List<int>();

        // Lista de IDs de professores associados a esta turma
        public List<int> TeacherIds { get; set; } = new List<int>();

        // Propriedades para exibir informações dos alunos e professores
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public IEnumerable<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
