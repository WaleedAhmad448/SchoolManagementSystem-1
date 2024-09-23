using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Attendance : IEntity
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

        // Data da aula
        [Required]
        public DateTime Date { get; set; }

        // Presença: Presente ou Ausente
        public bool Presence { get; set; }

        // Comentários adicionais (opcional)
        public string Remarks { get; set; }
    
    }
}
