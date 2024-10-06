using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SchoolManagementSystem.Data.Entities
{
    public class Teacher : IEntity
    {
        // Chave primária da tabela Teacher
        public int Id { get; set; }

        public string UserId { get; set; }

        // Propriedade de navegação para a entidade User associada
        public User User { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(50)]
        public string LastName { get; set; }

        // Grau acadêmico do professor (por exemplo, Bacharel, Mestrado)
        [Display(Name = "Academic Degree")]
        public AcademicDegree AcademicDegree { get; set; }

        // Data de contratação do professor, campo obrigatório
        [Display(Name = "Hire Date")]
        public DateTime? HireDate { get; set; }

        // String formatada para exibir a data de contratação
        public string FormattedHireDate => HireDate?.ToString("dd/MM/yyyy");

        // Status do professor (Ativo, Inativo, Pendente)
        [Display(Name = "Status")]
        public TeacherStatus Status { get; set; } = TeacherStatus.Active;

        // Coleção de turmas associadas a este professor através da tabela de junção
        public ICollection<TeacherSchoolClass> TeacherSchoolClasses { get; set; } = new List<TeacherSchoolClass>();

        // Coleção de disciplinas associadas a este professor através da entidade de junção
        public ICollection<TeacherSubject> TeacherSubjects { get; set; } = new List<TeacherSubject>();

        // ID da imagem para a foto de perfil do professor
        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        // Caminho completo da URL para a imagem de perfil do professor
        public string ImageFullPath => ImageId == Guid.Empty
            ? "https://schoolstorageaccount.blob.core.windows.net/images/noimage.png"
            : $"https://schoolstorageaccount.blob.core.windows.net/teachers/{ImageId}";
    }

    // Enum para padronizar o status do professor
    public enum TeacherStatus
    {
        Pending,
        Active,
        Inactive
    }
}
