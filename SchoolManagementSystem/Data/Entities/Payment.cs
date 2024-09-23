using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class Payment : IEntity
    {
        // Implementação da interface IEntity
        public int Id { get; set; }

        // Identificador do aluno (chave estrangeira)
        [Required]
        public int StudentId { get; set; }

        // Navegação para a entidade Student
        public Student Student { get; set; }

        // Valor pago
        [Required]
        [Range(0.01, 10000.00)]
        public decimal Amount { get; set; }

        // Data do pagamento
        [Required]
        public DateTime PaymentDate { get; set; }

        // Status do pagamento (Pendente, Pago, Atrasado)
        [MaxLength(20)]
        public string Status { get; set; }

        // Identificador da transação (opcional)
        [MaxLength(50)]
        public string TransactionId { get; set; }

        // Método de pagamento (Cartão, Transferência, etc.)
        [MaxLength(20)]
        public string PaymentMethod { get; set; }
    
    }
}
