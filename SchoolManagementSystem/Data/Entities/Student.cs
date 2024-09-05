using SchoolManagementSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class Student : IEntity
{
    // Identificador único do aluno. É a chave primária da tabela.
    public int Id { get; set; }

    // Identificador do utilizador associado a este aluno. É obrigatório.
    [Required]
    public string UserId { get; set; }

    // Navegação para a entidade `User`. Representa o utilizador associado ao aluno.
    public User User { get; set; }

    // Data de matrícula do aluno. Usada para saber quando o aluno foi matriculado.
    [Display(Name = "Enrollment Date")]
    public DateTime EnrollmentDate { get; set; }

    // Status do aluno. Pode ser algo como "Ativo" ou "Inativo". Tem um comprimento máximo de 20 caracteres.
    [MaxLength(20)]
    public string Status { get; set; }

    // Identificador da turma à qual o aluno está associado. É obrigatório.
    [Required]
    public int SchoolClassId { get; set; }

    // Navegação para a entidade `SchoolClass`. Representa a turma à qual o aluno pertence.
    public SchoolClass SchoolClass { get; set; }
}
