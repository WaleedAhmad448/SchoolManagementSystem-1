using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Teacher : IEntity
    {
        // Identificador único do professor. É a chave primária da tabela.
        public int Id { get; set; }

        // Identificador do utilizador associado ao professor. É obrigatório e serve como chave estrangeira.
        [Required]
        public string UserId { get; set; }

        // Navegação para a entidade `User`. Representa o utilizador associado ao professor.
        public User User { get; set; }

        // Grau acadêmico do professor. Este campo tem um comprimento máximo de 100 caracteres e é opcional.
        [MaxLength(100)]
        public string AcademicDegree { get; set; } // Ex: Mestrado, Doutorado

        // Departamento onde o professor trabalha. Este campo tem um comprimento máximo de 100 caracteres e é opcional.
        [MaxLength(100)]
        public string Department { get; set; }

        // Data de contratação do professor. Este campo é obrigatório.
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        // Coleção de disciplinas que o professor ensina. Representa o relacionamento de um-para-muitos com `Subject`.
        public ICollection<Subject> Subjects { get; set; } // Disciplinas ensinadas

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://schoolstorageaccount.blob.core.windows.net/images/noimage.png"
            //Caso tenha imagem
            : $"https://schoolstorageaccount.blob.core.windows.net/teachers{ImageId}";
    }
}
