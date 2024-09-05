using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Subject : IEntity
    {
        // Identificador único da disciplina. É a chave primária da tabela.
        public int Id { get; set; }

        // Nome da disciplina. Este campo é obrigatório e tem um comprimento máximo de 100 caracteres.
        [Required]
        [MaxLength(100)]
        public string SubjectName { get; set; }

        // Descrição da disciplina. Este campo é opcional.
        public string Description { get; set; }

        // Identificador do curso ao qual a disciplina está associada. É obrigatório e serve como chave estrangeira.
        [Required]
        public int CourseId { get; set; }

        // Navegação para a entidade `Course`. Representa o curso ao qual a disciplina pertence.
        public Course Course { get; set; }

        // Identificador do professor que ensina a disciplina. É obrigatório e serve como chave estrangeira.
        [Required]
        public int TeacherId { get; set; }

        // Navegação para a entidade `Teacher`. Representa o professor que ensina a disciplina.
        public Teacher Teacher { get; set; }

        // Identificador da turma onde a disciplina é lecionada. É obrigatório e serve como chave estrangeira.
        [Required]
        public int SchoolClassId { get; set; }

        // Navegação para a entidade `SchoolClass`. Representa a turma onde a disciplina é lecionada.
        public SchoolClass SchoolClass { get; set; }

        // Horário de início da disciplina. É obrigatório e representa o momento em que a disciplina começa.
        [Required]
        public DateTime StartTime { get; set; }

        // Horário de término da disciplina. É obrigatório e representa o momento em que a disciplina termina.
        [Required]
        public DateTime EndTime { get; set; }
    }
}
