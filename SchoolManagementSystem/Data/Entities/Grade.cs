using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Grade : IEntity
    {
        // Implementação da interface IEntity
        public int Id { get; set; }  // Esta será a chave primária, conforme exigido pela interface IEntity

        // Identificador do aluno (chave estrangeira)
        [Required]
        public int StudentId { get; set; }

        // Navegação para a entidade Student
        public Student Student { get; set; }

        // Identificador da disciplina (chave estrangeira)
        [Required]
        public int SubjectId { get; set; }

        // Navegação para a entidade Subject
        public Subject Subject { get; set; }

        // Valor da nota
        [Range(0, 100)]
        public decimal Value { get; set; }

        // Status da nota: Aprovado, Reprovado, Pendência
        [MaxLength(20)]
        public string Status { get; set; }

        // Data em que a nota foi registrada
        [Display(Name = "Date Recorded")]
        public DateTime DateRecorded { get; set; }
    
    }
}
