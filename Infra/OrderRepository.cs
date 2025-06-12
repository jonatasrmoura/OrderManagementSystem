using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Infra
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ConnectionContext _context;

        public OrderRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> FindAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> FindByIdAsync(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}