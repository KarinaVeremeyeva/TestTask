using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class OrderService : IOrderService
    {
        private ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder()
        {
            var maxOrderAmount = await _context.Orders
                .OrderByDescending(x => x.Price * x.Quantity)
                .FirstOrDefaultAsync();

            return maxOrderAmount;
        }

        public async Task<List<Order>> GetOrders()
        {
            var filteredOrders = await _context.Orders
                .Where(x => x.Quantity > 10)
                .ToListAsync();

            return filteredOrders;
        }
    }
}
