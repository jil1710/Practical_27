using Core_Practical_17.Models;

namespace Core_Practical_17.Repository
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);
        Task<bool> IsUserExist(string email);
        Task<List<string>> GetRoleByEmail(string email);
        Task<bool> CheckUserPassword(string email, string password);
    }
}