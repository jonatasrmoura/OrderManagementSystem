using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.DTOS
{
    public class CreateOrderDTO
    {
        [Required]
        [StringLength(100)]
        public string Client { get; set; }

        [Required]
        [StringLength(100)]
        public string Product { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Value { get; set; }
    }
}
