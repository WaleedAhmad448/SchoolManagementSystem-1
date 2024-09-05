using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    // Representa um curso dentro do sistema de gestão escolar.
    public class Course : IEntity
    {
        // Identificador único do curso. É a chave primária da tabela.
        public int Id { get; set; }

        // Nome do curso. É obrigatório e tem um comprimento máximo de 100 caracteres.
        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }

        // Descrição do curso. Não é obrigatória e pode ter comprimento variável.
        public string Description { get; set; }

        // Duração do curso em semanas. Deve estar entre 1 e 52 semanas.
        [Range(1, 52)]
        public int Duration { get; set; }

        // Número de créditos atribuídos ao curso. Usado para avaliar a carga de trabalho do curso.
        public int Credits { get; set; }

        // Coleção de disciplinas associadas a este curso. 
        // Cada curso pode ter várias disciplinas.
        public ICollection<Subject> Subjects { get; set; }

        // Coleção de turmas associadas a este curso.
        // Cada curso pode ter várias turmas.
        public ICollection<SchoolClass> SchoolClasses { get; set; }
    }
}
