using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class SchoolClass : IEntity
    {
        // Identificador único da turma. É a chave primária da tabela.
        public int Id { get; set; }

        // Nome da turma. É obrigatório e tem um comprimento máximo de 50 caracteres.
        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        // Identificador do curso ao qual a turma está associada. É obrigatório.
        [Required]
        public int CourseId { get; set; }

        // Navegação para o curso ao qual a turma pertence.
        public Course Course { get; set; }

        // Data de início da turma. Usada para saber quando a turma começa.
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        // Data de fim da turma. Usada para saber quando a turma termina.
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        // Coleção de alunos associados a esta turma.
        // Cada turma pode ter vários alunos.
        public ICollection<Student> Students { get; set; }

        // Coleção de disciplinas associadas a esta turma.
        // Cada turma pode ter várias disciplinas.
        public ICollection<Subject> Subjects { get; set; }
    
    }
}
