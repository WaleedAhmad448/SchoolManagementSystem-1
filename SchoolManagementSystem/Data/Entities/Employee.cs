using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Employee : IEntity
    {
        // Implementação da interface IEntity
        public int Id { get; set; }

        // Relacionamento com a entidade User
        [Required]
        public string UserId { get; set; }

        // Navegação para a entidade User (associada ao funcionário)
        public User User { get; set; }

        // Cargo do funcionário
        [MaxLength(50)]
        public string Department { get; set; }

        // Data de contratação do funcionário
        [Required]
        public DateTime HireDate { get; set; }

        // Telefone do funcionário
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://supershop88.azurewebsites.net/images/noimage.png"
            //Caso tenha imagem
            : $"https://supershopsi88.blob.core.windows.net/products/{ImageId}";
    }
}
