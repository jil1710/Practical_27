using Core_Practical_17.DataContext;
using Core_Practical_17.Models;
using Core_Practical_17.Repository;
using Microsoft.EntityFrameworkCore;

namespace Core_Practical_17.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> IsUserExist(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CheckUserPassword(string email,string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();    
            return true;
        }

        public async Task<List<string>> GetRoleByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            var role = await _dbContext.UserRoles.Where(e => e.UserId == user!.Id).Join(_dbContext.Roles,usr => usr.RoleId,rol=> rol.Id, (usr,rol)=> new {usr,rol}).Select(e=> e.rol.RoleName).ToListAsync();

            return role;
        }


    }
}
