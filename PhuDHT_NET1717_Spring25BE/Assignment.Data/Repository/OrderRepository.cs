using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Data.Base;
using Assignment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Data.Repository
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository() { }
        public OrderRepository(TutoringKidDbContext context) => _context = context;

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            /* return await _dbSet
                 .Include(u => u.Role.RoleName) // Assuming User entity has a navigation property to Role
                 .ToListAsync();*/
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order> GetByIdOrderAsync(Guid id)
        {
            return await _context.Orders.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
