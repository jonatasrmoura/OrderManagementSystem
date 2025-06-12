using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models
{
    public enum OrderStatus
    {
        Pendente,
        Processando,
        Finalizado
    }

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Client { get; set; }

        [Required]
        public string Product { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pendente;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}