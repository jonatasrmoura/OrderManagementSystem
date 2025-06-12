using OrderManagementSystem.DTOS;

namespace OrderManagementSystem.Models
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
        Task<List<Order>> FindAllAsync();
        Task<Order?> FindByIdAsync(Guid id);
        Task<Order> UpdateAsync(Order order);
        Task<Boolean> DeleteAsync(Order order);
    }
}
