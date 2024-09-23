using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Alert : IEntity
    {
        // Implementação da interface IEntity
        public int Id { get; set; }

        // Mensagem do alerta
        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        // Data de criação do alerta
        [Required]
        public DateTime CreatedAt { get; set; }

        // Indica se o alerta foi resolvido ou não
        public bool IsResolved { get; set; }

        // Identificador do funcionário que enviou o alerta (chave estrangeira)
        [Required]
        public int EmployeeId { get; set; }

        // Navegação para a entidade Employee
        public Employee Employee { get; set; }
    
    }
}
