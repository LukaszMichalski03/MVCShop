using LoginRegisterIdentity.Data;
using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace LoginRegisterIdentity.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            this._context = context;
        }
        public bool Add(Order order)
        {
            _context.Orders.Add(order);
            return Save();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUser(string id)
        {
            List<Order> result = await _context.Orders
                .Where(o => o.UserId == id)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .ToListAsync();

            return result;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
