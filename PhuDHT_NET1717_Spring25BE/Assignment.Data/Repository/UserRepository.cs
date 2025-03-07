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
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository() { }
        public UserRepository(TutoringKidDbContext context) => _context = context;

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetByIdUserAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
