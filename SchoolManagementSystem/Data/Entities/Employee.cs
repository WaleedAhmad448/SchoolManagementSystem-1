using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Employee : IEntity
    {
        // Implementação da interface IEntity
        public int Id { get; set; }

        public string UserId { get; set; }

        // Navegação para a entidade User (associada ao funcionário)
        public User User { get; set; }

        // Cargo do funcionário
        [MaxLength(50)]
        public string Department { get; set; }

        public DateTime HireDate { get; set; }

        // Telefone do funcionário
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://schoolstorageaccount.blob.core.windows.net/images/noimage.png"
            //Caso tenha imagem
            : $"https://schoolstorageaccount.blob.core.windows.net/employees{ImageId}";
    }
}
