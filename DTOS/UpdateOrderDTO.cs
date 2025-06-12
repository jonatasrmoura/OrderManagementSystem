using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.DTOS
{
    public class UpdateOrderDTO : CreateOrderDTO
    {
        [Required]
        [FromQuery]
        [Display(Name = "Status do Pedido")]
        public OrderStatus Status { get; set; }
    }
}
