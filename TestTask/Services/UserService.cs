using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<User> GetUser()
        {
            var user = await _context.Users
                .Include(x => x.Orders)
                .OrderByDescending(x => x.Orders.Count)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            var inactiveUsers = await _context.Users
                .Where(x => x.Status == Enums.UserStatus.Inactive)
                .ToListAsync();

            return inactiveUsers;
        }
    }
}
